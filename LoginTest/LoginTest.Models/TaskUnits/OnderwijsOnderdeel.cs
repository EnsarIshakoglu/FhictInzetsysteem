using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Inzetsysteem.Models
{
    class OnderwijsOnderdeel : Task
    {
        public int BegroteUren { get; private set; }
        public int Factor { get; private set; }
    }
}
