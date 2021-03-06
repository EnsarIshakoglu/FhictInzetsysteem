﻿using Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [Authorize(Roles = "TeamLeader")]
    [Route("Home/Index")]
    public class TeamLeaderController : Controller
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