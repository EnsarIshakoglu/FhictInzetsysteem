﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class TeamLeader : Role
    {
        public Team Team { get; set; }
    }
}
