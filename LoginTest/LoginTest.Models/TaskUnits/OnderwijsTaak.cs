using System;
using System.Collections.Generic;
using System.Text;

namespace Inzetsysteem.Models
{
    public class OnderwijsTaak : Task
    {
        public string Periode { get; private set; }
        public int BegroteUren { get; private set; }
        public int Factor { get; private set; }
    }
}
