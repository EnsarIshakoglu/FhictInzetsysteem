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
        public IActionResult Index(string userName, string password)
        {
            var userLogic = new UserLogic();
            var user = new User(password, userName);

            if (userLogic.Login(user))
            {
                return View("Ingelogd");
            }
            else
            {
                return View();
            }
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
