using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace FHICTDeploymentSystem.Models
{
    public class FixateViewModel
    {
        public IEnumerable<EducationObject> Sections { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}
