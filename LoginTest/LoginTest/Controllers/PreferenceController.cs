﻿using System;
using System.Collections.Generic;
using System.Linq;
using FHICTDeploymentSystem.Logic;
using FHICTDeploymentSystem.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;

namespace FHICTDeploymentSystem.Controllers
{
    public class PreferenceController : Controller
    {
        private readonly PreferenceLogic _preferenceLogic = new PreferenceLogic();
        private readonly AddTaskLogic _addTaskLogic = new AddTaskLogic();

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


        public void SaveSectionPreferences(IEnumerable<Preference> preferences)
        {
            List<Preference> savePreferences = new List<Preference>();

            foreach (var preference in preferences)
            {
                if (preference.Value != -1)
                {
                    var value = preference.Value;
                    savePreferences.Add(new Preference {Task = preference.Task, Value = value});
                }
            }

            _preferenceLogic.SaveSectionPreferences(savePreferences, Convert.ToInt32(User.Identity.Name));
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

        public void SaveUnitPreferences(IEnumerable<Preference> preferences)
        {
            List<Preference> savePreferences = new List<Preference>();

            foreach (var preference in preferences)
            {
                if (preference.Value != -1)
                {
                    var value = preference.Value;
                    savePreferences.Add(new Preference {Task = preference.Task, Value = value});
                }
            }

            _preferenceLogic.SaveUnitPreferences(savePreferences, Convert.ToInt32(User.Identity.Name));
        }
        [HttpGet]
        public IActionResult UnitExecutionPreference(int unitExecId)
        {
            var preferences = new List<Preference>();
            var unitExecs = _addTaskLogic.GetUnitTermExecutions(unitExecId);

            foreach (var unitExec in unitExecs)
            {
                preferences.Add(_preferenceLogic.GetUnitExecPreference(unitExec, Convert.ToInt32(User.Identity.Name)));
            }

            return View("SubmitPreferences", preferences);
        }

        public void SaveUnitExecutionPreferences(IEnumerable<Preference> preferences)
        {
            List<Preference> savePreferences = new List<Preference>();

            foreach (var preference in preferences)
            {
                if (preference.Value != -1)
                {
                    var value = preference.Value;
                    savePreferences.Add(new Preference { Task = preference.Task, Value = value });
                }
            }

            _preferenceLogic.SaveUnitExecutionPreferences(savePreferences, Convert.ToInt32(User.Identity.Name));
        }

        [HttpGet]
        public IActionResult TaskPreference(int unitExecId)
        {
            var preferences = new List<Preference>();
            var tasks = _preferenceLogic.GetAllTasks(unitExecId);

            foreach (var task in tasks)
            {
                preferences.Add(_preferenceLogic.GetTaskPreference(task, Convert.ToInt32(User.Identity.Name)));
            }

            return View("SubmitPreferences", preferences);
        }

        [HttpPost]
        public void SaveTaskPreferences(IEnumerable<Preference> preferences)
        {
            List<Preference> savePreferences = new List<Preference>();

            foreach (var preference in preferences)
            {
                if (preference.Value != -1)
                {
                    var value = preference.Value;
                    savePreferences.Add(new Preference {Task = preference.Task, Value = value});
                }
            }

            _preferenceLogic.SaveTaskPreferences(savePreferences, Convert.ToInt32(User.Identity.Name));
        }

        public IActionResult RedirectLayer(EducationType educationType, int id)
        {
            if (educationType.Equals(EducationType.Section))
            {
                TempData["Title"] = "Units";
                return UnitPreference(id);
            }
            if (educationType.Equals(EducationType.Unit))
            {
                TempData["Title"] = "UnitExecutions";
                return UnitExecutionPreference(id);
            }
            if (educationType.Equals(EducationType.UnitExec))
            {
                TempData["Title"] = "Tasks";
                return TaskPreference(id);
            }
                return null;
        }

        [HttpPost]
        public IActionResult SaveChecker([FromBody]IEnumerable<Preference>preferences)
        {
            var returnRoute = RedirectToAction("SectionPreference", "Preference");
            var taskType = preferences.First().Task.EducationType;

            if (taskType.Equals(EducationType.Section))
            {
                SaveSectionPreferences(preferences);
                returnRoute = RedirectToAction("SectionPreference", "Preference");
            }
            if (taskType.Equals(EducationType.Unit))
            {
                SaveUnitPreferences(preferences);
                returnRoute = RedirectToAction("UnitPreference", "Preference");
                //return RedirectToAction("UnitPreference", "Preference");
            }
            if (taskType.Equals(EducationType.UnitExec))
            {
                SaveUnitExecutionPreferences(preferences);
                returnRoute = RedirectToAction("UnitExecutionPreference", "Preference");
                //return RedirectToAction("UnitExecutionPreference", "Preference");
            }
            if (taskType.Equals(EducationType.Task))
            {
                SaveTaskPreferences(preferences);
                returnRoute = RedirectToAction("TaskPreference", "Preference");
                //return RedirectToAction("TaskPreference", "Preference");
            }

            return returnRoute;
        }
    }
}