using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using Inzetsysteem.Logic;
using Microsoft.AspNetCore.Mvc;
using Inzetsysteem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Inzetsysteem.Controllers
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
            //return RedirectToAction("Login", "Home");
            return User.Identity.IsAuthenticated ? View("Profile") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([Bind("Password, Username")] User user)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            if (_userLogic.Login(user))
            {
                InitUser(user, _userLogic.GetUserID(user));
                return RedirectToAction("Profile", "Home");   //De cookies worden pas nadat je naar een nieuwe controller bent gegaan gerefreshed, hierdoor doe ik redirecten naar de index pag van homecontroller
            }
            return View("Index");
        }
        
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult MijnTaken()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VoorkeurInvoeren()
        {
            return RedirectToAction("Index", "Preference");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logout()
        {
            LogOut();
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

        private async void LogOut()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
