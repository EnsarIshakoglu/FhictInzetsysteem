using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Contexts;
using Models;
using Models.Enums;

namespace Logic
{
    public class AlgorithmLogic
    {
        private readonly AlgorithmContext _context = new AlgorithmContext();
        public List<EducationObject> AllTasks { get; private set; }
        public IEnumerable<EducationObject> FixedTasks { get; private set; }
        public IEnumerable<Employee> Employees { get; private set; }

        public IEnumerable<EducationObject> StartAlgorithm()
        {
            GetAllData();
            foreach (var task in AllTasks)
            {
                task.Factor -= FixedTasks.Count(t => t.Id == task.Id);
                if (task.Factor > 0)
                {
                    List<Employee> tempEmployeeList = GetCompetentEmployees(task);
                    AddValueToEmployeePreferences(tempEmployeeList, task);
                    AddPointsToEmployeesUsingCompetences(tempEmployeeList);
                    AddPointsToEmployeesUsingAvailability(tempEmployeeList, task);
                    AssignTask(tempEmployeeList, task);
                }
            }

            return AllTasks.Where(t => t.Factor > 0).ToList();
        }

        private void GetAllData()
        {
            AllTasks = _context.GetAllTasks();
            FixedTasks = _context.GetAllAssignedTasks();
            Employees = _context.GetAllEmployees();
            foreach (var employee in Employees)
            {
                employee.Competences = _context.GetEmployeeCompetences(employee.Id);
                employee.Preferences = _context.GetEmployeePreferences(employee.Id);
            }
        }

        private List<Employee> GetCompetentEmployees(EducationObject task)
        {
            List<Employee> tempEmployeeList = new List<Employee>();

            foreach (var employee in Employees)
            {
                if (employee.Competences.Any(e => e.Id == task.Id))
                {
                    tempEmployeeList.Add(new Employee()
                    {
                        Id = employee.Id,
                        OpenHours = employee.OpenHours,
                        MaxOvertime = employee.MaxOvertime,
                        Preferences = employee.Preferences,
                        Competences = employee.Competences
                    });
                }
            }

            return tempEmployeeList;
        }

        private void AddValueToEmployeePreferences(IEnumerable<Employee> tempEmployeeList, EducationObject task)
        {
            var enumValues = Enum.GetValues(typeof(PreferencePoints));

            foreach (var employee in tempEmployeeList)
            {
                int preferenceValue = 0;
                if (employee.Preferences.FirstOrDefault(p => p.Task.Equals(task)) != null)
                {
                    preferenceValue = employee.Preferences.First(p => p.Task.Equals(task)).Value;
                }

                employee.Points += (int)enumValues.GetValue(preferenceValue);
            }
        }

        private void AddPointsToEmployeesUsingCompetences(IEnumerable<Employee> tempEmployeeList)
        {
            List<Employee> sortedEmployeeList = tempEmployeeList.OrderByDescending(e => e.Competences.Count()).ToList();
            int points = 1;
            int factor = (50 / Math.Max(1, sortedEmployeeList.Count()));
            foreach (var employee in sortedEmployeeList)
            {
                tempEmployeeList.First(e => e.Id == employee.Id).Points += (points * factor);
                points++;
            }
        }

        private void AddPointsToEmployeesUsingAvailability(IEnumerable<Employee> tempEmployeeList, EducationObject task)
        {
            foreach (var employee in tempEmployeeList)
            {
                var openHours = employee.OpenHours[task.Period - 1];
                int points = 0;
                if (openHours > 0)
                {
                    points = openHours / 10;
                }
                else
                {
                    points = openHours;
                }

                employee.Points += points;
            }
        }

        private void AssignTask(IEnumerable<Employee> tempEmployeeList, EducationObject task)
        {
            List<Employee> sortedEmployeeList = tempEmployeeList.OrderByDescending(e => e.Points).ToList();

            int factor = task.Factor;
            var selectedEmployees = sortedEmployeeList.Where(e =>
                e.OpenHours[task.Period - 1] - task.EstimatedHours >= -e.MaxOvertime[task.Period - 1]).Take(factor);
            
            foreach (var employee in selectedEmployees)
            {
                _context.AssignTask(task, employee);
                Employees.First(e => e.Id == employee.Id).OpenHours[task.Period - 1] -= task.EstimatedHours;
                task.Factor--;
            }
        }
    }
}
