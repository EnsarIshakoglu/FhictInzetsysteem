using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class Employee
    {
        public int Id { get; set; }
        public IEnumerable<Preference> Preferences { get; set; }
        public IEnumerable<EducationObject> Competences { get; set; }
        public int[] OpenHours { get; set; }
        public int Points { get; set; } = 0;

    }
}
