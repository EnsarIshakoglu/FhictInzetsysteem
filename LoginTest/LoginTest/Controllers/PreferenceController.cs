using System;
using System.Collections.Generic;
using Inzetsysteem.Logic;
using Inzetsysteem.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inzetsysteem.Controllers
{
    public class PreferenceController : Controller
    {
        private readonly int _userId;
        private readonly PreferenceLogic _preferenceLogic = new PreferenceLogic();

        public PreferenceController()
        {
            _userId = Convert.ToInt32(User.Identity.Name);
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult OnderwijsTrajectPreference()
        {
            var preferences = new List<Preference>();
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
            List<Preference> preferences = new List<Preference>();

            foreach (var traject in _preferenceLogic.GetAllOnderwijsTrajecten())
            {
                var preferenceValue = Request.Form[traject.Naam].ToString();
                int value = Convert.ToInt16(preferenceValue);                               //todo convert.toint vervangen met iets netters

                preferences.Add(new Preference{Taak = traject, Waarde = value});
            }

            _preferenceLogic.SaveTrajectPreferences(preferences, Convert.ToInt32(User.Identity.Name));

            return RedirectToAction("OnderwijsTrajectPreference", "Preference");
        }
    }
}