using System;
using System.Collections.Generic;
using System.Text;

namespace Inzetsysteem.Models
{
    public class OnderwijsTraject : Taak
    {
        public OnderwijsTraject(string naam)
        {
            Naam = naam;
        }
        public OnderwijsTraject(string naam, int id)
        {
            Naam = naam;
            Id = id;
        }
        public string Test { get; set; }
    }
}
