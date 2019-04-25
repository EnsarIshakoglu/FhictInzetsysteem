using System;
using System.Collections.Generic;
using System.Text;

namespace Inzetsysteem.Models
{
    public class Preference
    {
        public Task Taak { get; set; }
        public int Waarde { get; set; } = -1;
        public bool WaardeIsAverage { get; set; } = false;
    }
}
