using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class EducationObject
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Period { get; set; }
        public int EstimatedHours { get; set; }
        public int Factor { get; set; }
        public string Description { get; set; }
        public string Explanation { get; set; }
        public EducationType EducationType { get; set; }
        public int UnitExecId { get; set; }
        public string TermExecution { get; set; }
    }
}
