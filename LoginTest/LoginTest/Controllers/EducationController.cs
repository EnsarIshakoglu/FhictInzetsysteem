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
        private readonly AddTaskLogic _addTaskLogic = new AddTaskLogic();

        public IActionResult Index()
        {
            return View("AddTask", _preferenceLogic.GetAllSections());
        }

        [HttpPost]
        public IActionResult AddTask([FromBody] EducationObject taskToAdd)
        {
            /*for (var x = 0; x < taskToAdd.Factor; x++)
            {*/
                _addTaskLogic.AddTask(taskToAdd);               //als factor 2 is komt die taak 2x in de db terecht?
            /*}*/

            return RedirectToAction("Profile", "Home");
        }

        public IActionResult EditTask()
        {
            return View(_preferenceLogic.GetAllSections());
        }

        [HttpPost]
        public IActionResult GetTasks(int execId)
        {
            return Json(_preferenceLogic.GetAllTasks(execId));
        }

        [HttpPost]
        public IActionResult GetUnits(int sectionId)
        {
            var result = new List<EducationObject>();

            foreach (var unit in _preferenceLogic.GetAllUnits(sectionId))
            {
                result.Add(unit);
            }
            return Json(result);
        }

        [HttpPost]
        public IActionResult GetTermExecutions(int unitId)
        {
            var result = new List<EducationObject>();

            foreach (var termExecution in _addTaskLogic.GetUnitTermExecutions(unitId))
            {
                result.Add(termExecution);
            }
            return Json(result);
        }

        [HttpPost]
        public IActionResult RemoveTask([FromBody] EducationObject task)
        {
            _addTaskLogic.RemoveTask(task);

            return new JsonResult(new { message = "Success" });
        }

        public IActionResult UpdateTask(string jsonObject)
        {
            var task = JsonConvert.DeserializeObject<EducationObject>(jsonObject);

            _addTaskLogic.UpdateTask(task);

            return View("EditTask", _preferenceLogic.GetAllSections());
        }

        public IActionResult GetTaskFromId(int taskId)
        {
            var task = new EducationObject
            {
                Id = taskId
            };
            task = _addTaskLogic.GetTaskById(task);
            return View("ActualEditTask", task);
        }
    }
}
