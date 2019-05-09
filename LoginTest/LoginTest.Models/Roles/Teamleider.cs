using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inzetsysteem.Models
{
    public class Teamleider : Role
    {
        public Team Team { get; private set; }
    }
}
