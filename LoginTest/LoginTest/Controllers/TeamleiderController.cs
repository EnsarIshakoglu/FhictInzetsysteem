using Inzetsysteem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inzetsysteem.Controllers
{
    [Authorize(Roles = "TeamLeader")]
    [Route("Home/Index")]
    public class TeamleiderController : Controller
    {
        public IActionResult Teachers()
        {
            return View();
        }

        public IActionResult ManageTeams()
        {
            return View();
        }
    }
}