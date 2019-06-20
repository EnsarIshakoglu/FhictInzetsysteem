using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace FHICTDeploymentSystem.Models
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public IEnumerable<EducationObject> Competences { get; set; }
    }
}
