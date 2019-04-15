using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inzetsysteem.Models
{
    public class Docent : Role
    {
        public Team Team { get; private set; }
        public int OpenInzet { get; private set; }
        public List<Preference> Voorkeuren { get; private set; }
        public List<Task> Bekwaamheden { get; private set; }
        public List<Task> toegewezenTaken { get; private set; }
    }
}
