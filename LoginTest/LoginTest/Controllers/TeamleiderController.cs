﻿using FHICTDeploymentSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FHICTDeploymentSystem.Controllers
{
    [Authorize(Roles = "Teamleider")]
    [Route("Home/Index")]
    public class TeamleiderController : Controller
    {
        public IActionResult Docenten()
        {
            return View();
        }

        public IActionResult TeamBeheren()
        {
            return View();
        }
    }
}