using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FHICTDeploymentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Logic;
using Models;

namespace FHICTDeploymentSystem.Controllers
{
    public class AlgorithmController : Controller
    {
        private readonly AlgorithmLogic _algorithmLogic = new AlgorithmLogic();

        [HttpGet]
        public IActionResult Deploy()
        {
            var viewModel = new AlgorithmViewModel
            {
                Employees = _algorithmLogic.GetAllAssignedTasks(),
                Tasks = _algorithmLogic.GetAllLeftOverTasks()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult StartAlgorithm()
        {
            _algorithmLogic.StartAlgorithm();

            return RedirectToAction("Deploy");
        }
    }
}