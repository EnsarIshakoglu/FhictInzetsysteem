using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using LoginTest.Logic;
using Microsoft.AspNetCore.Mvc;
using LoginTest.Models;
using Microsoft.AspNetCore.Authorization;

namespace LoginTest.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Index([Bind("Password, Username")] User user)
        {
            var userLogic = new UserLogic();

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (userLogic.Login(user))
            {
                userLogic.InitUser(user, User);

                return View("Ingelogd");
            }

            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
