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
        public IActionResult SectionPreference()
        {
            var preferences = new List<Preference>();
            var sectionen = _preferenceLogic.GetAllSectionen();

            foreach (var Section in sectionen)
            {
                preferences.Add(_preferenceLogic.GetsectionPreference(Section, Convert.ToInt32(User.Identity.Name)));
            }

            return View("SubmitPreferences", preferences);
        }

        [HttpPost]
        public IActionResult GetsectionPreferences()
        {
            List<Preference> preferences = new List<Preference>();

            foreach (var section in _preferenceLogic.GetAllSectionen())
            {
                var preferenceValue = Request.Form[section.Name].ToString();
                int value = Convert.ToInt16(preferenceValue);                               //todo convert.toint vervangen met iets netters

                preferences.Add(new Preference { Task = section, Value = value });
            }

            _preferenceLogic.SaveEdSectionPreferences(preferences, Convert.ToInt32(User.Identity.Name));

            return RedirectToAction("SectionPreference", "Preference");
        }

        [HttpPost]
        public IActionResult UnitPreference(int sectionId)
        {
            var preferences = new List<Preference>();
            var eenheden = _preferenceLogic.GetAllUnits(sectionId);

            foreach (var Unit in eenheden)
            {
                preferences.Add(_preferenceLogic.GetEdUnitPreference(Unit, Convert.ToInt32(User.Identity.Name)));
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

            foreach (var section in _preferenceLogic.GetAllSectionen())
            {
                var preferenceValue = Request.Form[section.Name].ToString();
                int value = Convert.ToInt16(preferenceValue);                               //todo convert.toint vervangen met iets netters

                preferences.Add(new Preference { Task = section, Value = value });
            }

            _preferenceLogic.SaveEdSectionPreferences(preferences, Convert.ToInt32(User.Identity.Name));

            return RedirectToAction("UnitPreference", "Preference");
        }

        public IActionResult RedirectLayer(string TaskName, int id)
        {
            if (TaskName == typeof(Section).Name)
            {
                return UnitPreference(id);
            }
            else if (TaskName == typeof(Unit).Name)
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