using System;
using System.Collections.Generic;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;

namespace FHICTDeploymentSystem.Controllers
{
    public class TaskController : Controller
    {
        private readonly PreferenceLogic _preferenceLogic = new PreferenceLogic();
        private readonly TaskLogic _taskLogic = new TaskLogic();

        public IActionResult AddTask()
        {
            return View("AddTask", _preferenceLogic.GetAllSections());
        }

        [HttpPost]
        public IActionResult AddTask([FromBody] EducationObject taskToAdd)
        {
            _taskLogic.AddTask(taskToAdd);

            return RedirectToAction("AddTask", "Task");
        }

        public IActionResult TaskSelector()
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
        public IActionResult GetUnitTermExecutions(int unitId)
        {
            var result = new List<EducationObject>();

            foreach (var termExecution in _taskLogic.GetUnitTermExecutions(unitId))
            {
                result.Add(termExecution);
            }
            return Json(result);
        }

        [HttpPost]
        public IActionResult RemoveTask([FromBody] EducationObject task)
        {
            _taskLogic.RemoveTask(task);

            return new JsonResult(new { message = "Success" });
        }

        public IActionResult UpdateTask(string jsonObject)
        {
            var task = JsonConvert.DeserializeObject<EducationObject>(jsonObject);

            _taskLogic.UpdateTask(task);

            return View("TaskSelector", _preferenceLogic.GetAllSections());
        }

        public IActionResult GetTaskFromId(int taskId)
        {
            var task = new EducationObject
            {
                Id = taskId
            };
            task = _taskLogic.GetTaskById(task);
            return View("ActualEditTask", task);
        }

    
    }
}