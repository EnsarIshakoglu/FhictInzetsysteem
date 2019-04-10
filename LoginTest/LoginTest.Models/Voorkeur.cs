using System;
using System.Collections.Generic;
using System.Text;

namespace Inzetsysteem.Models
{
    public class Voorkeur
    {
        public Voorkeur(Taak taak, int waarde)
        {
            Taak = taak;
            Waarde = waarde;
        }
        public Voorkeur(Taak taak)
        {
            Taak = taak;
        }
        public Taak Taak { get; set; }
        public int Waarde { get; set; } = -1;
    }
}
