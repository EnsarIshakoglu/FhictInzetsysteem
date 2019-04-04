using System.Diagnostics;
using LoginTest.Logic;
using Microsoft.AspNetCore.Mvc;
using LoginTest.Models;

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
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("Password, Username")] User user)
        {
            var userLogic = new UserLogic();

            if (ModelState.IsValid)
            {
                if (userLogic.Login(user))
                {
                    return View("Ingelogd");
                }
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
