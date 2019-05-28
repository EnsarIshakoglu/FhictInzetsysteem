using System;
using System.Collections.Generic;
using System.Text;
using DAL.Contexts;
using FHICTDeploymentSystem.DAL.Contexts;
using FHICTDeploymentSystem.Models;

namespace DAL
{
    public class TeamRepository
    {
        private readonly ITeamContext _context;

        public TeamRepository()
        {
            _context = new TeamContext();
        }

        public IEnumerable<User> GetTeamUsers(User user)
        {
            return _context.GetTeamUsers(user);
        }

        public void RemoveUser(User _user)
        {
            _context.RemoveUser(_user);
        }

        public void EditUserInTeam(User user)
        {
            _context.EditUserInTeam(user);
        }
        
    }
}
