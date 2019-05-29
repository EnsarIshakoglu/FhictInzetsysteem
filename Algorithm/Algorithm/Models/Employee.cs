﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class Employee
    {
        public int Id { get; set; }
        public IEnumerable<Preference> Preferences { get; set; }
        public IEnumerable<Competence> Competences { get; set; }
        public int Hours { get; set; }
        public int Points { get; set; }

    }
}
