using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace FHICTDeploymentSystem.Models
{
    public class ManageUnitTermExecViewModel
    {
        public IEnumerable<EducationObject> Sections { get; set; }
        public IEnumerable<EducationObject> TermExecs { get; set; }
        public IEnumerable<EducationObject> Units { get; set; }
    }
}
