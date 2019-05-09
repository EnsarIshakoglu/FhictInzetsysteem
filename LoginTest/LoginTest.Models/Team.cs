using System;
using System.Collections.Generic;
using System.Text;

namespace Inzetsysteem.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public int AantalMensen { get; private set; }
    }
}
