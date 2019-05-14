using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            GetTasksPreferences(tasks, preferences, userId);
            var preference = new Preference { Value = CalcAveragePreference(preferences), Task = section, ValueIsAverage = true };
            return preference;
        }

        public Preference GetUnitPreference(EducationObject unit, int userId)
        {
            List<Preference> preferences = new List<Preference>();

            var tasks = GetAllTasks(unit.Id);

            GetTasksPreferences(tasks, preferences, userId);

            var preference = new Preference {Value = CalcAveragePreference(preferences), Task = unit, ValueIsAverage = true };
            return preference;
        }

        public Preference GetTaskPreference(EducationObject task, int userId)
        {
            var preference = new Preference{Value = _repo.CheckTaskPreference(task, userId).Value, Task = task};
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
                    Name = sectionPreference.Task.Name
                }));

                foreach (var task in tasks)
                {
                    var taskPreference = CheckTaskPreference(task, userId);
                    if (taskPreference.Value == -1)
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
