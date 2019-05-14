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


        public IActionResult SaveSectionPreferences(IEnumerable<Preference> preferences)
        {
            List<Preference> savePreferences = new List<Preference>();

            foreach (var preference in preferences)
            {
                var value = preference.Value;
                savePreferences.Add(new Preference {Task = preference.Task, Value = value});
            }

            _preferenceLogic.SaveSectionPreferences(savePreferences, Convert.ToInt32(User.Identity.Name));

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

        public IActionResult SaveUnitPreferences(IEnumerable<Preference> preferences)
        {
            List<Preference> savePreferences = new List<Preference>();

            foreach (var preference in preferences)
            {
                var value = preference.Value;
                savePreferences.Add(new Preference { Task = preference.Task, Value = value });
            }

            _preferenceLogic.SaveUnitPreferences(savePreferences, Convert.ToInt32(User.Identity.Name));

            return RedirectToAction("SectionPreference", "Preference");
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

        public IActionResult SaveTaskPreferences(IEnumerable<Preference> preferences)
        {
            List<Preference> savePreferences = new List<Preference>();

            foreach (var preference in preferences)
            {
                var value = preference.Value;
                savePreferences.Add(new Preference { Task = preference.Task, Value = value });
            }

            _preferenceLogic.SaveTaskPreferences(savePreferences, Convert.ToInt32(User.Identity.Name));

            return RedirectToAction("SectionPreference", "Preference");
        }

        public IActionResult RedirectLayer(EducationType educationType, int id)
        {
            if (educationType.Equals(EducationType.Section))
            {
                TempData["Title"] = "Units";
                return UnitPreference(id);
            }
            else if (educationType.Equals(EducationType.Unit))
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
        public IActionResult SaveChecker([FromBody]IEnumerable<Preference>preferences)
        {
            var taskType = preferences.First().Task.EducationType;

            if (taskType == EducationType.Section)
            {
                return SaveSectionPreferences(preferences);
            }
            else if (taskType == EducationType.Unit)
            {
                return SaveUnitPreferences(preferences);
            }
            else
            {
                return SaveTaskPreferences(preferences);
            }
        }
    }
}