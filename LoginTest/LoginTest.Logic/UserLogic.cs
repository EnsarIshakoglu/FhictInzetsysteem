﻿using System.Collections.Generic;
using System.Security.Claims;
using FHICTDeploymentSystem.DAL;
using FHICTDeploymentSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace FHICTDeploymentSystem.Logic
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
    }
}
