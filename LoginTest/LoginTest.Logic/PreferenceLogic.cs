﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FHICTDeploymentSystem.DAL;
using FHICTDeploymentSystem.Models;
using Newtonsoft.Json;

namespace FHICTDeploymentSystem.Logic
{
    public class PreferenceLogic
    {
        private readonly PreferenceRepository _repo = new PreferenceRepository();

        public IEnumerable<EducationObject> GetAllSections()
        {
            return _repo.GetAllSections();
        }

        public Preference GetSectionPreference(EducationObject section, int userId)
        {
            List<Preference> preferences = new List<Preference>();
            var tasks = GetTasksFromSection(section);

            preferences = GetTasksPreferences(tasks, preferences, userId);
            int averageValue = CalcAveragePreference(preferences);
            var preference = new Preference { Value = averageValue, Task = section };
            preference.ValueIsAverage = CheckIfAverage(preferences, averageValue);

            return preference;
        }

        public Preference GetUnitPreference(EducationObject unit, int userId) //---------------------------------------------
        {
            List<Preference> preferences = new List<Preference>();
            var tasks = GetTasksFromUnit(unit.Id);

            preferences = GetTasksPreferences(tasks, preferences, userId);
            int averageValue = CalcAveragePreference(preferences);
            var preference = new Preference { Value = averageValue, Task = unit };
            preference.ValueIsAverage = CheckIfAverage(preferences, averageValue);

            return preference;
        }
        public Preference GetUnitExecPreference(EducationObject unitExec, int userId)
        {
            List<Preference> preferences = new List<Preference>();
            var tasks = GetAllTasks(unitExec.Id);

            preferences = GetTasksPreferences(tasks, preferences, userId);
            int averageValue = CalcAveragePreference(preferences);
            var preference = new Preference { Value = averageValue, Task = unitExec };
            preference.ValueIsAverage = CheckIfAverage(preferences, averageValue);

            return preference;
        }

        private IEnumerable<EducationObject> GetTasksFromUnit(int unitId)
        {
            return _repo.GetTasksFromUnit(unitId);
        }

        public bool CheckIfAverage(List<Preference> preferences, int averageValue)
        {
            foreach (var pref in preferences)
            {
                if (pref.Value != averageValue)
                {
                    return true;
                }
            }
            return false;
        }

        public Preference GetTaskPreference(EducationObject task, int userId)
        {
            var preference = new Preference { Value = _repo.CheckTaskPreference(task, userId).Value, Task = task };
            return preference;
        }


        public List<Preference> GetTasksPreferences(IEnumerable<EducationObject> tasks, List<Preference> preferences, int userId)
        {
            foreach (var task in tasks)
            {
                var priority = CheckTaskPreference(task, userId);
                if (priority.Value > 0)
                {
                    preferences.Add(priority);
                }
            }
            return preferences;
        }

        public int CalcAveragePreference(List<Preference> preferences)
        {
            decimal preferenceValue = 0;
            decimal valueToDivideBy = preferences.Count();
            if (valueToDivideBy == 0) valueToDivideBy = 1;

            foreach (var preference in preferences)
            {
                preferenceValue += preference.Value;
            }

            var averagePreferenceValue = preferenceValue / valueToDivideBy;

            return (int)Math.Round(averagePreferenceValue);
        }

        public void SaveSectionPreferences(IEnumerable<Preference> sectionPreferences, int userId)
        {
            foreach (var sectionPreference in sectionPreferences)
            {
                List<EducationObject> tasks = new List<EducationObject>();

                tasks.AddRange(GetTasksFromSection(new EducationObject
                {
                    Id = sectionPreference.Task.Id,
                }));

                foreach (var task in tasks)
                {
                    var checkTaskPreference = CheckTaskPreference(task, userId);
                    if (checkTaskPreference.Value == -1)
                    {
                        AddTaskPreference(task, sectionPreference.Value, userId);
                    }
                    else
                    {
                        UpdateTaskPreference(task, sectionPreference.Value, userId);
                    }
                }
            }
        }

        public void SaveUnitPreferences(IEnumerable<Preference> unitPreferences, int userId)//---------------------------
        {
            foreach (var unitPreference in unitPreferences)
            {
                List<EducationObject> tasks = new List<EducationObject>();

                tasks.AddRange(GetTasksFromUnit(unitPreference.Task.Id));
                
                foreach (var task in tasks)
                {
                    var checkTaskPreference = CheckTaskPreference(task, userId);
                    if (checkTaskPreference.Value == -1)
                    {
                        AddTaskPreference(task, unitPreference.Value, userId);
                    }
                    else
                    {
                        UpdateTaskPreference(task, unitPreference.Value, userId);
                    }
                }
            }
        }
        public void SaveUnitExecutionPreferences(IEnumerable<Preference> unitExecutionPreferences, int userId)//---------------------------
        {
            foreach (var unitExecutionPreference in unitExecutionPreferences)
            {
                List<EducationObject> tasks = new List<EducationObject>();

                tasks.AddRange(GetAllTasks(unitExecutionPreference.Task.Id));

                foreach (var task in tasks)
                {
                    var checkTaskPreference = CheckTaskPreference(task, userId);
                    if (checkTaskPreference.Value == -1)
                    {
                        AddTaskPreference(task, unitExecutionPreference.Value, userId);
                    }
                    else
                    {
                        UpdateTaskPreference(task, unitExecutionPreference.Value, userId);
                    }
                }
            }
        }

        public void SaveTaskPreferences(IEnumerable<Preference> taskPreferences, int userId)
        {
            List<EducationObject> tasks = new List<EducationObject>();
            foreach (var taskPreference in taskPreferences)
            {
                var checkTaskPreference = CheckTaskPreference(taskPreference.Task, userId);
                if (checkTaskPreference.Value == -1)
                {
                    AddTaskPreference(taskPreference.Task, taskPreference.Value, userId);
                }
                else
                {
                    UpdateTaskPreference(taskPreference.Task, taskPreference.Value, userId);
                }
            }
        }

        public Preference CheckTaskPreference(EducationObject task, int userId)
        {
            return _repo.CheckTaskPreference(task, userId);
        }

        public void AddTaskPreference(EducationObject task, int priority, int userId)
        {
            _repo.AddTaskPreference(task, priority, userId);
        }

        public void UpdateTaskPreference(EducationObject task, int priority, int userId)
        {
            _repo.UpdateTaskPreference(task, priority, userId);
        }

        public IEnumerable<EducationObject> GetAllUnits(int edSectionId)
        {
            return _repo.GetAllUnits(edSectionId);
        }

        public IEnumerable<EducationObject> GetAllTasks(int unitExecId)
        {
            return _repo.GetAllTasks(unitExecId);
        }

        public IEnumerable<EducationObject> GetTasksFromSection(EducationObject section)
        {
            return _repo.GetTasksFromSection(section);
        }
    }
}
