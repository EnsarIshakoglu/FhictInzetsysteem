using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FHICTDeploymentSystem.Models
{
    public class TeacherModel
    {
        public int ID { get; set; }
        public IEnumerable<EducationObject> Bewkaamheden { get; set; }
        public EducationObject Uren { get; set; }
        public User TeacherInfo { get; set; }
    }
}
