using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Teacher : Role
    {
        public Team Team { get; set; }
        public int OpenHours { get; set; }
        public List<Preference> Preferences { get; set; }
        public List<EducationObject> Competencies { get; set; }
        public List<EducationObject> AssignedTasks { get; set; }
    }
}
