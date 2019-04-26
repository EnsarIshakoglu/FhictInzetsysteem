using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inzetsysteem.Logic;
using Inzetsysteem.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Inzetsysteem.Controllers
{
    public class TaakController : Controller
    {
        public IActionResult TaakToevoegen()
        {
            OnderwijsLogic onderwijsLogic = new OnderwijsLogic();
            List<OnderwijsTraject> onderwijsTrajects = onderwijsLogic.GetAllTrajects();
            return View(onderwijsTrajects);
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

        [HttpPost]
        public IActionResult GetEenheden(string traject)
        {
            OnderwijsLogic b = new OnderwijsLogic();
            List<OnderwijsEenheid> q = new List<OnderwijsEenheid>();
            foreach (OnderwijsEenheid eenheid in b.GetAllEenhedenByTraject(new OnderwijsTraject { Naam = traject }))
            {
                q.Add(eenheid);
            }
            /*string c = JsonConvert.SerializeObject(q);
            System.IO.File.WriteAllText(@"D:\path.txt", c);*/
            return Json(q);
        }

    }
}