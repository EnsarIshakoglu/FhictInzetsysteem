using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logic;
using Models;

namespace FHICTDeploymentSystem.Controllers
{
    public class AlgorithmController : Controller
    {
        private readonly AlgorithmLogic _algorithmLogic = new AlgorithmLogic();
        private IEnumerable<EducationObject> leftOverTasks = new List<EducationObject>();

        [HttpGet]
        public IActionResult Deploy()
        {
            return View(leftOverTasks);
        }

        [HttpPost]
        public IActionResult StartAlgorithm()
        {
            leftOverTasks = _algorithmLogic.StartAlgorithm();

            return View("Deploy", leftOverTasks);
        }
    }
}