using System;
using System.Collections.Generic;
using System.Text;
using FHICTDeploymentSystem.Models;

namespace FHICTDeploymentSystem.DAL
{
    public class UserRepository
    {
        private readonly IUserContext _context;

        public UserRepository()
        {
            _context = new UserContext();
        }

        public bool Login(User user)
        {
            return _context.Login(user);
        }

        public IEnumerable<string> GetUserRoles(User user)
        {
            return _context.GetUserRoles(user);
        }

        public int GetUserId(User user)
        {
            return _context.GetUserId(user);
        }
    }
}
