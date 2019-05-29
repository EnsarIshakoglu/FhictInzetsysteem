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

        public void GetAllData()
        {
            AllTasks = _context.GetAllTasks();
            Employees = _context.GetAllEmployees();
            foreach (var employee in Employees)
            {
                employee.Competences = _context.GetEmployeeCompetences(employee.Id);
                employee.Preferences = _context.GetEmployeePreferences(employee.Id);
            }
        }


    }
}
