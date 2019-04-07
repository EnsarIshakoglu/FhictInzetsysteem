using System;
using System.Collections.Generic;
using System.Text;
using LoginTest.Models;

namespace LoginTest.DAL
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

        public List<string> InitUser(User user)
        {
            return _context.InitUser(user);
        }
    }
}
