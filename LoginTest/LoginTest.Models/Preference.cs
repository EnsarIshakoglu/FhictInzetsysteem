using System;
using System.Collections.Generic;
using System.Text;

namespace FHICTDeploymentSystem.Models
{
    public class Preference
    {
        public ParentTask Task { get; set; }
        public int Value { get; set; } = -1;
        public bool ValueIsAverage { get; set; } = false;
    }
}
