using System;
using System.Collections.Generic;
using System.Text;

namespace FHICTDeploymentSystem.Models
{
    public abstract class ParentTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Period { get; set; }
        public int EstimatedHours { get; set; }
        public int Factor { get; set; }
        public string Description { get; set; }
        public string Explanation { get; set; }
        public int ExecId { get; set; }
    }
}
