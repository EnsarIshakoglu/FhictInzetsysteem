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

        public IEnumerable<Section> GetAllSectionen()
        {
            return _repo.GetAllSections();
        }

        public Preference GetsectionPreference(Section edSection, int userId)
        {
            List<Preference> preferences = new List<Preference>();

            var tasks = _repo.GetTasksFromEdSection(edSection);

            GetTasksPreferences(tasks, preferences, userId);
            var preference = new Preference { Value = CalcAveragePreference(preferences), Task = edSection, ValueIsAverage = true };
            return preference;
        }

        public Preference GetEdUnitPreference(Unit EdUnitId, int userId)
        {
            List<Preference> preferences = new List<Preference>();

            var tasks = _repo.GetAllTasks(EdUnitId.Id);

            GetTasksPreferences(tasks, preferences, userId);

            var preference = new Preference {Value = CalcAveragePreference(preferences), Task = EdUnitId, ValueIsAverage = true };
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

        public void SaveEdSectionPreferences(List<Preference> preferences, int userId)
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

        public IEnumerable<Task> GetAllTasks(int EdUnitId)
        {
            return _repo.GetAllTasks(EdUnitId);
        }

        public IEnumerable<Task> GetTasksFromEdSection(Section edSection)
        {
            return _repo.GetTasksFromEdSection(edSection);
        }
    }
}
