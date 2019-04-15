using System;
using System.Collections.Generic;
using System.Text;

namespace Inzetsysteem.Models
{
    public class Preference
    {
        public Preference(Task taak, int waarde)
        {
            Taak = taak;
            Waarde = waarde;
        }
        public Preference(Task taak)
        {
            Taak = taak;
        }
        public Task Taak { get; set; }
        public int Waarde { get; set; } = -1;
    }
}
