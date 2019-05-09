


using System;
using System.Collections.Generic;
using System.Text;

namespace FHICTDeploymentSystem.Models
{
    public class Task : ParentTask
    {
        public int Period { get; set; }
        public int EstimatedHours { get; set; }
        public int Factor { get; private set; }
        public string Description { get; set; }
        public string Explanation { get; set; }
    }
}
