using Inzetsysteem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inzetsysteem.Controllers
{
    [Authorize(Roles = "Teamleider")]
    [Route("Home/Index")]
    public class TeamleiderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}