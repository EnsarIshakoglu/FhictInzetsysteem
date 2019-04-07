using System;
using System.Collections.Generic;
using System.Security.Claims;
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
            return _userRepository.Login(user);
        }

        public void InitUser(User user, ClaimsPrincipal claimsPrincipal)
        {
            var roles = new List<string>();
            var claims = new List<Claim>();

            roles = _userRepository.InitUser(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            claimsPrincipal.AddIdentity(new ClaimsIdentity(claims));
        }
    }
}
