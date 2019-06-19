using System.Collections.Generic;
using System.Security.Claims;
using DAL;
using Models;

namespace Logic
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
            return _userRepository.Login(user);
        }

        public IEnumerable<string> GetUserRoles(User user)
        {
            return _userRepository.GetUserRoles(user);
        }

        public int GetUserId(User user)
        {
            return _userRepository.GetUserId(user);
        }

        public User GetAllEmployeeData(int userId)
        {
            return _userRepository.GetAllEmployeeData(userId);
        }
    }
}
