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
        public IActionResult EducationSectionPreference()
        {
            var preferences = new List<Preference>();
            var trajecten = _preferenceLogic.GetAllEducationSectionen();

            foreach (var EducationSection in trajecten)
            {
                preferences.Add(_preferenceLogic.GetTrajectPreference(EducationSection, Convert.ToInt32(User.Identity.Name)));
            }

            return View("SubmitPreferences", preferences);
        }

        [HttpPost]
        public IActionResult GetTrajectPreferences()
        {
            List<Preference> preferences = new List<Preference>();

            foreach (var traject in _preferenceLogic.GetAllEducationSectionen())
            {
                var preferenceValue = Request.Form[traject.Name].ToString();
                int value = Convert.ToInt16(preferenceValue);                               //todo convert.toint vervangen met iets netters

                preferences.Add(new Preference { Task = traject, Value = value });
            }

            _preferenceLogic.SaveEdSectionPreferences(preferences, Convert.ToInt32(User.Identity.Name));

            return RedirectToAction("EducationSectionPreference", "Preference");
        }

        [HttpPost]
        public IActionResult EducationUnitPreference(int trajectId)
        {
            var preferences = new List<Preference>();
            var eenheden = _preferenceLogic.GetAllEducationUnits(trajectId);

            foreach (var EducationUnit in eenheden)
            {
                preferences.Add(_preferenceLogic.GetEdUnitPreference(EducationUnit, Convert.ToInt32(User.Identity.Name)));
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

            foreach (var traject in _preferenceLogic.GetAllEducationSectionen())
            {
                var preferenceValue = Request.Form[traject.Name].ToString();
                int value = Convert.ToInt16(preferenceValue);                               //todo convert.toint vervangen met iets netters

                preferences.Add(new Preference { Task = traject, Value = value });
            }

            _preferenceLogic.SaveEdSectionPreferences(preferences, Convert.ToInt32(User.Identity.Name));

            return RedirectToAction("EducationUnitPreference", "Preference");
        }

        public IActionResult RedirectLayer(string TaskName, int id)
        {
            if (TaskName == typeof(Section).Name)
            {
                return EducationUnitPreference(id);
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