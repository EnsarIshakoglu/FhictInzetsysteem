﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using FHICTDeploymentSystem.Models;
using Logic;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserLogic _userLogic;

        public HomeController()
        {
            _userLogic = new UserLogic();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Profile");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index([Bind("Password, Username")] User user)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (_userLogic.Login(user))
            {
                InitUser(user, _userLogic.GetUserId(user));
                return RedirectToAction("Profile");   //De cookies worden pas nadat je naar een nieuwe controller bent gegaan gerefreshed, hierdoor doe ik redirecten naar de index pag van homecontroller
            }

            return View();

        }

        [HttpGet]
        public IActionResult Profile()
        {
            var userId = Convert.ToInt32(User.Identity.Name);
            var user = _userLogic.GetAllUserData(userId);
            return View(user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logout()
        {
            LogOut(Request.Cookies.Keys);

            return RedirectToAction("Index", "Home");
        }

        private async void InitUser(User user, int userId)
        {
            var claims = new List<Claim>();

            var roles = _userLogic.GetUserRoles(user);

            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            claims.Add(new Claim(ClaimTypes.Name, userId.ToString()));

            ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            var authProp = new AuthenticationProperties
            {
                IsPersistent = false
            };

            await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProp);
        }
        private async void LogOut(IEnumerable<string> keys)
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
