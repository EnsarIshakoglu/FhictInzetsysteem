using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inzetsysteem.Models
{
    public class Teacher : Role
    {
        public Team Team { get; set; }
        public int OpenHours { get; set; }
        public List<Preference> Preferences { get; set; }
        public List<ParentTask> Competencies { get; set; }
        public List<ParentTask> AssignedTasks { get; set; }
    }
}
