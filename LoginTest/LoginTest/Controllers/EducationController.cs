using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FHICTDeploymentSystem.Logic;
using FHICTDeploymentSystem.Models;
using Logic;
using Newtonsoft.Json;

namespace FHICTDeploymentSystem.Controllers
{
    public class EducationController : Controller
    {
        private readonly PreferenceLogic _preferenceLogic = new PreferenceLogic();

        

        public IActionResult AddTermExec()
        {
            return View("AddTermExec", _preferenceLogic.GetAllSections());
        }
    }
}
