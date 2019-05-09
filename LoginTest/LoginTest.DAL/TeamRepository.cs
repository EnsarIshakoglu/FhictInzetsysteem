using System;
using System.Collections.Generic;
using System.Text;
using Inzetsysteem.DAL.Contexts;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL
{
    public class TeamRepository
    {
        private readonly ITeamContext _context;

        public TeamRepository()
        {
            _context = new TeamContext();
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _context.GetAllTeams();
        }

        public IEnumerable<User> GetTeamUsers(User user)
        {
            return _context.GetTeamUsers(user);
        }
    }
}
