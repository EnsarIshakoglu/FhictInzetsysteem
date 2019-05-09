using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inzetsysteem.DAL;
using Inzetsysteem.Models;
using Newtonsoft.Json;

namespace Inzetsysteem.Logic
{
    public class PreferenceLogic
    {
        private readonly PreferenceRepository _repo = new PreferenceRepository();

        public IEnumerable<OnderwijsTraject> GetAllOnderwijsTrajecten()
        {
            return _repo.GetAllOnderwijsTrajecten();
        }

        public Preference GetTrajectPreference(OnderwijsTraject traject, int userId)
        {
            List<Preference> preferences = new List<Preference>();

            var tasks = _repo.GetTakenFromTraject(traject);

            GetTasksPreferences(tasks, preferences, userId);
            var preference = new Preference { Waarde = CalcAveragePreference(preferences), Taak = traject, WaardeIsAverage = true };
            return preference;
        }

        public Preference GetEenheidPreference(OnderwijsEenheid EdUnitId, int userId)
        {
            List<Preference> preferences = new List<Preference>();

            var tasks = _repo.GetAllTasks(EdUnitId.Id);

            GetTasksPreferences(tasks, preferences, userId);

            var preference = new Preference {Waarde = CalcAveragePreference(preferences), Taak = EdUnitId, WaardeIsAverage = true };
            return preference;
        }

        public Preference GetTaskPreference(OnderwijsTaak task, int userId)
        {
            var preference = new Preference{Waarde = _repo.CheckTaskPreference(task, userId).Waarde, Taak = task};
            return preference;
        }


        public List<Preference> GetTasksPreferences(IEnumerable<OnderwijsTaak> tasks, List<Preference> preferences, int userId)
        {
            foreach (var task in tasks)
            {
                var value = _repo.CheckTaskPreference(task, userId);
                if (value.Waarde > 0)
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
                preferenceValue += preference.Waarde;
            }

            preferenceValue = preferenceValue / valueToDivideBy;

            return preferenceValue;
        }

        public void SaveTrajectPreferences(List<Preference> preferences, int userId)
        {
            _repo.SaveTrajectPreferences(preferences, userId);
        }

        public Preference CheckTaskPreference(OnderwijsTaak task, int userId)
        {
            return _repo.CheckTaskPreference(task, userId);
        }

        public void AddTaskPreference(OnderwijsTaak task, int priority, int userId)
        {
            _repo.AddTaskPreference(task, priority, userId);
        }

        public void UpdateTaskPreference(OnderwijsTaak task, int priority, int userId)
        {
            _repo.UpdateTaskPreference(task, priority, userId);
        }

        public IEnumerable<OnderwijsEenheid> GetAllOnderwijsEenheden(int trajectId)
        {
            return _repo.GetAllOnderwijsEenheden(trajectId);
        }

        public IEnumerable<OnderwijsTaak> GetAllTasks(int EdUnitId)
        {
            return _repo.GetAllTasks(EdUnitId);
        }

        public IEnumerable<OnderwijsTaak> GetTasksFromTraject(OnderwijsTraject onderwijsTraject)
        {
            return _repo.GetTakenFromTraject(onderwijsTraject);
        }
    }
}
