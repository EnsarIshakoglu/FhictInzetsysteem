using System;
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

        public Preference GetUnitPreference(EducationObject unit, int userId)
        {
            List<Preference> preferences = new List<Preference>();
            var tasks = GetAllTasks(unit.Id);

            preferences = GetTasksPreferences(tasks, preferences, userId);
            int averageValue = CalcAveragePreference(preferences);
            var preference = new Preference { Value = averageValue, Task = unit };
            preference.ValueIsAverage = CheckIfAverage(preferences, averageValue);

            return preference;
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
                var Priority = CheckTaskPreference(task, userId);
                if (Priority.Value > 0)
                {
                    preferences.Add(Priority);
                }
            }
            return preferences;
        }

        public int CalcAveragePreference(List<Preference> preferences)
        {
            int preferenceValue = 0;
            int valueToDivideBy = preferences.Count();
            if (valueToDivideBy == 0) valueToDivideBy = 1;

            foreach (var preference in preferences)
            {
                preferenceValue += preference.Value;
            }

            preferenceValue = preferenceValue / valueToDivideBy;

            return preferenceValue;
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

        public void SaveUnitPreferences(IEnumerable<Preference> UnitPreferences, int userId)
        {
            foreach (var unitPreference in UnitPreferences)
            {
                List<EducationObject> tasks = new List<EducationObject>();

                tasks.AddRange(GetAllTasks(unitPreference.Task.Id));
                
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

        public IEnumerable<EducationObject> GetAllTasks(int unitId)
        {
            return _repo.GetAllTasks(unitId);
        }

        public IEnumerable<EducationObject> GetTasksFromSection(EducationObject section)
        {
            return _repo.GetTasksFromSection(section);
        }
    }
}
