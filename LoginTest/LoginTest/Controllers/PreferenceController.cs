using System;
using System.Collections.Generic;
using System.Linq;
using FHICTDeploymentSystem.Logic;
using FHICTDeploymentSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace FHICTDeploymentSystem.Controllers
{
    public class PreferenceController : Controller
    {
        private readonly PreferenceLogic _preferenceLogic = new PreferenceLogic();


        [HttpGet]
        public IActionResult SectionPreference()
        {
            var preferences = new List<Preference>();
            var sections = _preferenceLogic.GetAllSections();

            foreach (var section in sections)
            {
                preferences.Add(_preferenceLogic.GetSectionPreference(section, Convert.ToInt32(User.Identity.Name)));
            }

            TempData["Title"] = "Sections";
            return View("SubmitPreferences", preferences);
        }


        public IActionResult SaveSectionPreferences()
        {
            List<Preference> preferences = new List<Preference>();

            foreach (var section in _preferenceLogic.GetAllSections())
            {
                var preferenceValue = Request.Form[section.Id.ToString()].ToString();
                int value = Convert.ToInt32(preferenceValue);                               //todo convert.toint vervangen met iets netters

                preferences.Add(new Preference { Task = section, Value = value });
            }

            _preferenceLogic.SaveSectionPreferences(preferences, Convert.ToInt32(User.Identity.Name));

            return RedirectToAction("SectionPreference", "Preference");
        }

        [HttpGet]
        public IActionResult UnitPreference(int sectionId)
        {
            var preferences = new List<Preference>();
            var units = _preferenceLogic.GetAllUnits(sectionId);

            foreach (var unit in units)
            {
                preferences.Add(_preferenceLogic.GetUnitPreference(unit, Convert.ToInt32(User.Identity.Name)));
            }

            return View("SubmitPreferences", preferences);
        }

        public IActionResult SaveUnitPreferences(IEnumerable<Preference> unitPreferences)
        {
            List<Unit> units = new List<Unit>();
            List<Preference> preferences = new List<Preference>();
            foreach (var preference in unitPreferences)
            {
                units.Add((Unit)preference.Task);
            }

            foreach (var unit in units)
            {
                var preferenceValue = Request.Form[unit.Name].ToString();
                int value = Convert.ToInt16(preferenceValue);

                preferences.Add(new Preference { Task = unit, Value = value });
            }

            _preferenceLogic.SaveSectionPreferences(preferences, Convert.ToInt32(User.Identity.Name));

            return RedirectToAction("UnitPreference", "Preference");
        }

        [HttpPost]
        public IActionResult TaskPreference(int unitId)
        {
            var preferences = new List<Preference>();
            var tasks = _preferenceLogic.GetAllTasks(unitId);

            foreach (var task in tasks)
            {
                preferences.Add(_preferenceLogic.GetTaskPreference(task, Convert.ToInt32(User.Identity.Name)));
            }

            return View("SubmitPreferences", preferences);
        }

        public IActionResult SaveTaskPreferences(IEnumerable<Preference> taskPreferences)
        {
            List<Task> tasks = new List<Task>();
            List<Preference> preferences = new List<Preference>();
            foreach (var preference in taskPreferences)
            {
                tasks.Add((Task)preference.Task);
            }

            foreach (var task in tasks)
            {
                var preferenceValue = Request.Form[task.Name].ToString();
                int value = Convert.ToInt16(preferenceValue);

                preferences.Add(new Preference { Task = task, Value = value });
            }

            _preferenceLogic.SaveSectionPreferences(preferences, Convert.ToInt32(User.Identity.Name));

            return RedirectToAction("UnitPreference", "Preference");
        }

        public IActionResult RedirectLayer(string taskName, int id)
        {
            if (taskName == typeof(Section).Name)
            {
                TempData["Title"] = "Units";
                return UnitPreference(id);
            }
            else if (taskName == typeof(Unit).Name)
            {
                TempData["Title"] = "Tasks";
                return TaskPreference(id);
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public IActionResult SaveChecker(IEnumerable<Preference> preferences)
        {
            var taskType = preferences.First().Task.GetType();
            if (taskType == typeof(Section))
            {
                return RedirectToAction("SaveSectionPreferences", preferences);
            }
            else if (taskType == typeof(Unit))
            {
                return RedirectToAction("SaveUnitPreferences", preferences);
            }
            else
            {
                return RedirectToAction("SaveTaskPreferences", preferences);
            }
        }
    }
}