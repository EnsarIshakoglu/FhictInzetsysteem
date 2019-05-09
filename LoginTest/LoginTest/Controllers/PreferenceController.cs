using System;
using System.Collections.Generic;
using System.Linq;
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
            var preferences = new List<Preference>();
            var trajecten = _preferenceLogic.GetAllOnderwijsTrajecten();

            foreach (var onderwijsTraject in trajecten)
            {
                preferences.Add(_preferenceLogic.GetTrajectPreference(onderwijsTraject, Convert.ToInt32(User.Identity.Name)));
            }

            return View("SubmitPreferences", preferences);
        }

        [HttpPost]
        public IActionResult GetTrajectPreferences()
        {
            List<Preference> preferences = new List<Preference>();

            foreach (var traject in _preferenceLogic.GetAllOnderwijsTrajecten())
            {
                var preferenceValue = Request.Form[traject.Naam].ToString();
                int value = Convert.ToInt16(preferenceValue);                               //todo convert.toint vervangen met iets netters

                preferences.Add(new Preference { Taak = traject, Waarde = value });
            }

            _preferenceLogic.SaveTrajectPreferences(preferences, Convert.ToInt32(User.Identity.Name));

            return RedirectToAction("OnderwijsTrajectPreference", "Preference");
        }

        [HttpPost]
        public IActionResult OnderwijsEenheidPreference(int trajectId)
        {
            var preferences = new List<Preference>();
            var eenheden = _preferenceLogic.GetAllOnderwijsEenheden(trajectId);

            foreach (var onderwijsEenheid in eenheden)
            {
                preferences.Add(_preferenceLogic.GetEenheidPreference(onderwijsEenheid, Convert.ToInt32(User.Identity.Name)));
            }

            return View("SubmitPreferences", preferences);
        }

        [HttpPost]
        public IActionResult TaskPreference(int EdUnitId)
        {
            var preferences = new List<Preference>();
            var tasks = _preferenceLogic.GetAllTasks(EdUnitId);

            foreach (var task in tasks)
            {
                preferences.Add(_preferenceLogic.GetTaskPreference(task, Convert.ToInt32(User.Identity.Name)));
            }

            return View("SubmitPreferences", preferences);
        }

        [HttpPost]
        public IActionResult GetEenheidPreferences()
        {
            List<Preference> preferences = new List<Preference>();

            foreach (var traject in _preferenceLogic.GetAllOnderwijsTrajecten())
            {
                var preferenceValue = Request.Form[traject.Naam].ToString();
                int value = Convert.ToInt16(preferenceValue);                               //todo convert.toint vervangen met iets netters

                preferences.Add(new Preference { Taak = traject, Waarde = value });
            }

            _preferenceLogic.SaveTrajectPreferences(preferences, Convert.ToInt32(User.Identity.Name));

            return RedirectToAction("OnderwijsEenheidPreference", "Preference");
        }

        public IActionResult RedirectLayer(string taakNaam, int id)
        {
            if (taakNaam == typeof(OnderwijsTraject).Name)
            {
                return OnderwijsEenheidPreference(id);
            }
            else if (taakNaam == typeof(OnderwijsEenheid).Name)
            {
                return TaskPreference(id);
            }
            else
            {
                return null;
            }
        }
    }
}