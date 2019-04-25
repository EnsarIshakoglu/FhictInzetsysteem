using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Inzetsysteem.Models
{
    public class OnderwijsOnderdeel : Task
    {
        public int BegroteUren { get; private set; }
        public int Factor { get; private set; }
        public bool HasTaken { get; set; }
    }
}
