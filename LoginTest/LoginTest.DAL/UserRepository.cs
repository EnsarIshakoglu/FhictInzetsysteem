using System;
using System.Collections.Generic;
using System.Text;

namespace LoginTest.DAL
{
    public class UserRepository
    {
        private readonly IUserContext _context;

        public UserRepository()
        {
            _context = new UserContext();
        }

        public bool Login(string userName, string password)
        {
            return _context.Login(userName, password);
        }
    }
}
