using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FHICTDeploymentSystem.Logic;
using FHICTDeploymentSystem.Models;
using Task = System.Threading.Tasks.Task;

namespace FHICTDeploymentSystem.Controllers
{
    public class TaskController : Controller
    {
        PreferenceLogic preferenceLogic = new PreferenceLogic();
        public IActionResult Index()
        {
            return View(preferenceLogic.GetAllSections());
        }

        [HttpPost]
        public IActionResult AddTask(Models.Task task)
        {
            preferenceLogic.AddTask(task);

            return RedirectToAction("Profile", "Home");
        }

        [HttpPost]
        public IActionResult GetUnits(int sectionId)
        {
            List<Unit> result = new List<Unit>();
            foreach (Unit unit in preferenceLogic.GetAllUnits(sectionId))
            {
                result.Add(unit);
            }
            return Json(result);
        }

        public IActionResult GetExecs(int unitId)
        {
            List<Unit> result = new List<Unit>();
            /*foreach (Unit unit in preferenceLogic.(unitId)) //todo
            {
                result.Add(unit);
            }
            return Json(result);*/
            return null;
        }
    }
}