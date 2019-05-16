using System;
using System.Collections.Generic;
using System.Text;
using FHICTDeploymentSystem.DAL.Contexts;
using FHICTDeploymentSystem.Models;

namespace FHICTDeploymentSystem.DAL
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

        public void RemoveUser(User _user)
        {
            _context.RemoveUser(_user);
        }
    }
}
