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
        public IActionResult SaveSectionPreference()
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


        [HttpPost]
        public IActionResult GetSectionPreferences()
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

        [HttpPost]
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

        [HttpPost]
        public IActionResult GetUnitPreferences()
        {
            List<Preference> preferences = new List<Preference>();

            foreach (var section in _preferenceLogic.GetAllSections())
            {
                var preferenceValue = Request.Form[section.Name].ToString();
                int value = Convert.ToInt16(preferenceValue);                               //todo convert.toint vervangen met iets netters

                preferences.Add(new Preference { Task = section, Value = value });
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
    }
}