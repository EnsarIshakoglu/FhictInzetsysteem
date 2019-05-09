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

        public IEnumerable<Section> GetAllSections()
        {
            return _repo.GetAllSections();
        }

        public Preference GetSectionPreference(Section section, int userId)
        {
            List<Preference> preferences = new List<Preference>();

            var tasks = _repo.GetTasksFromSection(section);

            GetTasksPreferences(tasks, preferences, userId);
            var preference = new Preference { Value = CalcAveragePreference(preferences), Task = section, ValueIsAverage = true };
            return preference;
        }

        public Preference GetUnitPreference(Unit unitId, int userId)
        {
            List<Preference> preferences = new List<Preference>();

            var tasks = _repo.GetAllTasks(unitId.Id);

            GetTasksPreferences(tasks, preferences, userId);

            var preference = new Preference {Value = CalcAveragePreference(preferences), Task = unitId, ValueIsAverage = true };
            return preference;
        }

        public Preference GetTaskPreference(Task task, int userId)
        {
            var preference = new Preference{Value = _repo.CheckTaskPreference(task, userId).Value, Task = task};
            return preference;
        }


        public List<Preference> GetTasksPreferences(IEnumerable<Task> tasks, List<Preference> preferences, int userId)
        {
            foreach (var task in tasks)
            {
                var value = _repo.CheckTaskPreference(task, userId);
                if (value.Value > 0)
                {
                    preferences.Add(value);
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

        public void SaveSectionPreferences(List<Preference> preferences, int userId)
        {
            _repo.SaveEdSectionPreferences(preferences, userId);
        }

        public Preference CheckTaskPreference(Task task, int userId)
        {
            return _repo.CheckTaskPreference(task, userId);
        }

        public void AddTaskPreference(Task task, int priority, int userId)
        {
            _repo.AddTaskPreference(task, priority, userId);
        }

        public void UpdateTaskPreference(Task task, int priority, int userId)
        {
            _repo.UpdateTaskPreference(task, priority, userId);
        }

        public IEnumerable<Unit> GetAllUnits(int edSectionId)
        {
            return _repo.GetAllUnits(edSectionId);
        }

        public IEnumerable<Task> GetAllTasks(int unitId)
        {
            return _repo.GetAllTasks(unitId);
        }

        public IEnumerable<Task> GetTasksFromSection(Section section)
        {
            return _repo.GetTasksFromSection(section);
        }
    }
}
