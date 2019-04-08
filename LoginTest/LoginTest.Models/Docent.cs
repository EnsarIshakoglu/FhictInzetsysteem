using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inzetsysteem.Models
{
    public class Docent : Rol
    {
        //public team Team { get; private set; }
        public int OpenInzet { get; private set; }
        //public List<Voorkeur> Voorkeuren { get; private set; }
        public List<Taak> Bekwaamheden { get; private set; }
        public List<Taak> toegewezenTaken { get; private set; }
    }
}
