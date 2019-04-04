using System;
using System.Collections.Generic;
using System.Text;
using LoginTest.DAL;
using LoginTest.Models;

namespace LoginTest.Logic
{
    public class UserLogic
    {
        private readonly UserRepository _userRepository;

        public UserLogic()
        {
            _userRepository = new UserRepository();
        }

        public bool Login(User user)
        {
            return _userRepository.Login(user.Username, user.Password);
        }
    }
}
