using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inzetsysteem.DAL;
using Inzetsysteem.Models;
using Newtonsoft.Json;

namespace Inzetsysteem.Logic
{
    class TeamLogic
    {
        private readonly TeamRepository _repo = new TeamRepository();

        public IEnumerable<User> GetTeamUsers()
        {
            return GetTeamUsers();
        }

        public IEnumerable<Team> GetTeam(int id)
        {
            return GetTeam(id);
        }

    }
}
