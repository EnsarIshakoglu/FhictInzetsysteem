using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inzetsysteem.Logic;
using Inzetsysteem.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inzetsysteem.Controllers
{
    public class TaakController : Controller
    {
        public IActionResult Index()
        {
            OnderwijsLogic onderwijsLogic = new OnderwijsLogic();
            List<OnderwijsTraject> onderwijsTrajects = onderwijsLogic.GetAllTrajects();
            List<Taak> parentList = onderwijsTrajects.Cast<Taak>().ToList();
            return View(parentList);
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
    }
}