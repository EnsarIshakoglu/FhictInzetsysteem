using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Enums;

namespace Algorithm
{
    public class Logic
    {
        private readonly Context _context = new Context();
        public IEnumerable<EducationObject> AllTasks { get; private set; }
        public IEnumerable<EducationObject> AssignedTasks { get; private set; }
        public IEnumerable<Employee> Employees { get; private set; }

        public void StartAlgorithm()
        {
            GetAllData();
            foreach (var task in AllTasks)
            {
                List<Employee> tempEmployeeList = GetCompetentEmployees(task);
                AddValueToEmployeePreferences(tempEmployeeList, task);
                AddPointsToEmployeesUsingCompetences(tempEmployeeList);
            }
        }

        private void GetAllData()
        {
            AllTasks = _context.GetAllTasks();
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
            
        }
    }
}
