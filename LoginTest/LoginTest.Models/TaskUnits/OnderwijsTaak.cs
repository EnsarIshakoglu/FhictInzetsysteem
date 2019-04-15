using System;
using System.Collections.Generic;
using System.Text;

namespace Inzetsysteem.Models
{
    public class OnderwijsTaak : Taak
    {
        public string Periode { get; set; }
        public int BegroteUren { get; set; }
        public int Factor { get; set; }
    }
}
