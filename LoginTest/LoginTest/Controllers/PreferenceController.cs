using System;
using System.Collections.Generic;
using Inzetsysteem.Logic;
using Inzetsysteem.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inzetsysteem.Controllers
{
    public class PreferenceController : Controller
    {
        private readonly PreferenceLogic _preferenceLogic = new PreferenceLogic();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult OnderwijsTrajectPreference()
        {
            var preferences = new List<Voorkeur>();
            var trajecten = _preferenceLogic.GetAllOnderwijsTrajecten();

            foreach (var onderwijsTraject in trajecten)
            {
                preferences.Add(_preferenceLogic.GetTrajectPreference(onderwijsTraject, User.Identity.Name));
            }

            return View(preferences);
        }

        [HttpPost]
        public IActionResult GetTrajectPreferences()
        {
            List<Voorkeur> voorkeuren = new List<Voorkeur>();

            foreach (var traject in _preferenceLogic.GetAllOnderwijsTrajecten())
            {
                var voorkeurWaarde = Request.Form[traject.Naam].ToString();
                int waarde = Convert.ToInt16(voorkeurWaarde);

                voorkeuren.Add(new Voorkeur(traject, waarde));
            }

            //sla voorkeuren op

            return View("OnderwijsTrajectPreference");
        }
    }
}