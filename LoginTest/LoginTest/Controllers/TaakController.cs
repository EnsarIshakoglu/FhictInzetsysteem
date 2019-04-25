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
        public IActionResult TaakToevoegen()
        {
            OnderwijsLogic onderwijsLogic = new OnderwijsLogic();
            List<OnderwijsTraject> onderwijsTrajects = onderwijsLogic.GetAllTrajects();
            List<Taak> taakList = onderwijsTrajects.Cast<Taak>().ToList();
            //taakList.AddRange();
            return View(taakList);
        }

        public IActionResult OnderdeelToevoegen()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTask()
        {
            return RedirectToAction("taskView", "Taak");
        }

        [HttpPost]
        public IActionResult AddTaskPart([Bind("Periode, BegroteUren, Factor")] Taak taak)
        {
            return RedirectToAction("Index", "Taak");
        }
    }
}