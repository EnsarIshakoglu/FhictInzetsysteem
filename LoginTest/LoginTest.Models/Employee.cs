using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Preference> Preferences { get; set; }
        public IEnumerable<EducationObject> Competences { get; set; }
        public int[] OpenHours { get; set; }
        public int[] MaxOvertime { get; set; }
        public int Points { get; set; } = 0;
        public IEnumerable<EducationObject> AssignedTasks { get; set; }
    }
}