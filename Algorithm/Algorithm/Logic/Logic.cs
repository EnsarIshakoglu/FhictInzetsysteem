using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                List<Employee> tempList = GetCompetentEmployees(task);
                AddValueToEmployeePreferences(tempList);
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
            List<Employee> tempList = new List<Employee>();

            foreach (var employee in Employees)
            {
                if (CheckTaskCompetence(task, employee) || CheckUnitCompetence(task, employee) || CheckUnitExecCompetence(task, employee) || CheckSectionCompetence(task, employee))
                {
                    tempList.Add(employee);
                }
            }

            return tempList;
        }

        private bool CheckTaskCompetence(EducationObject task, Employee employee)
        {
            return employee.Competences.Any(e => e.TaskId == task.Id);
        }

        private bool CheckUnitCompetence(EducationObject task, Employee employee)
        {
            return employee.Competences.Any(e => e.UnitId == task.UnitId);
        }

        private bool CheckUnitExecCompetence(EducationObject task, Employee employee)
        {
            return employee.Competences.Any(e => e.UnitTermExecId == task.UnitExecId);
        }

        private bool CheckSectionCompetence(EducationObject task, Employee employee)
        {
            return employee.Competences.Any(e => e.SectionId == task.SectionId);
        }
    }
}
