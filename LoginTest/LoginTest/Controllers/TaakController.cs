using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inzetsysteem.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inzetsysteem.Controllers
{
    public class TaakController : Controller
    {
        public IActionResult Index()
        {
            //get list with trajecten
            return View();
        }

        public IActionResult taskView()
        {
            //get list with trajecten
            return View();
        }
        public IActionResult AddTask()
        {
            return RedirectToAction("taskView", "Taak");
        }

        public IActionResult AddTaskPart()
        {
            return RedirectToAction("Index", "Taak");
        }

        private List<OnderwijsTraject> getAllTrajects()
        {
            return null;
        }
    }
}