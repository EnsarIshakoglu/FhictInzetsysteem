using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Enums;
using Algorithm.Models;

namespace Algorithm
{
    public class Logic
    {
        private readonly Context _context = new Context();
        public List<EducationObject> AllTasks { get; private set; }
        public List<AssignedTask> AssignedTasks { get; private set; }
        public IEnumerable<EducationObject> FixedTasks { get; private set; }
        public IEnumerable<Employee> Employees { get; private set; }

        public void StartAlgorithm()
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
                    AssignTask(task);
                }
            }
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
                    tempEmployeeList.Add(employee);
                }
            }

            return tempEmployeeList;
        }

        private void AddValueToEmployeePreferences(IEnumerable<Employee> tempEmployeeList, EducationObject task)
        {
            var enumValues = Enum.GetValues(typeof(PreferencePoints));

            foreach (var employee in tempEmployeeList)
            {
                var preferenceValue = employee.Preferences.First(p => p.Task.Equals(task)).Value;

                employee.Points += (int)enumValues.GetValue(preferenceValue);
            }
        }

        private void AddPointsToEmployeesUsingCompetences(IEnumerable<Employee> tempEmployeeList)
        {
            List<Employee> sortedEmployeeList = tempEmployeeList.OrderByDescending(e => e.Competences.Count()).ToList();
            int points = 1;
            int factor = (50 / sortedEmployeeList.Count());
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
                var points = openHours / 10;

                employee.Points += points;
            }
        }

        private void AssignTask(EducationObject task)
        {
            List<Employee> sortedEmployeeList = Employees.OrderByDescending(e => e.Points).ToList();
            int factor = task.Factor;
            for (int i = 0; i < (factor - 1); i++)
            {
                if (i < sortedEmployeeList.Count)
                {
                    var employee = sortedEmployeeList[i];
                    AssignedTasks.Add(new AssignedTask()
                    {
                        Employee = employee,
                        Task = task
                    });
                    employee.OpenHours[task.Period - 1] -= task.EstimatedHours;
                    task.Factor--;
                }
            }
        }
    }
}
