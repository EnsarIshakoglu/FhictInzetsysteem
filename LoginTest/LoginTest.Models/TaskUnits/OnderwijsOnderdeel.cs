using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Inzetsysteem.Models
{
    public class OnderwijsOnderdeel : Taak
    {
        public int BegroteUren { get; set; }
        public int Factor { get; set; }
        public string OnderwijsEenheid { get; set; }
    }
}
