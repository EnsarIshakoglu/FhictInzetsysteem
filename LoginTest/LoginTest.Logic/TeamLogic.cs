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
    public class TeamLogic
    {
        private readonly TeamRepository _repo = new TeamRepository();

        public IEnumerable<User> GetTeamUsers(User user)
        {
            return GetTeamUsers(user);
        }

        public IEnumerable<Team> GetTeam(int id)
        {
            return GetTeam(id);
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _repo.GetAllTeams();
        }

    }
}
