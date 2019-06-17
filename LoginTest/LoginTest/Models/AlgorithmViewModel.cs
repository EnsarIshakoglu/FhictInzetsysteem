using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace FHICTDeploymentSystem.Models
{
    public class AlgorithmViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<EducationObject> Tasks { get; set; }
    }
}
