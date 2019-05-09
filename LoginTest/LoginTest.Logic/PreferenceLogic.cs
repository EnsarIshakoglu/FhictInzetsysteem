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

        public IEnumerable<EducationSection> GetAllOnderwijsTrajecten()
        {
            return _repo.GetAllOnderwijsTrajecten();
        }

        public Preference GetTrajectPreference(EducationSection traject, int userId)
        {
            List<Preference> preferences = new List<Preference>();

            var taken = _repo.GetTakenFromTraject(traject);
            var onderdelen = _repo.GetOnderdeelFromTraject(traject);

            GetTakenPreferences(taken, preferences, userId);
            GetOnderdelenPreferences(onderdelen, preferences, userId);
            var Preference = new Preference { Value = CalcAveragePreference(preferences), Task = traject, ValueIsAverage = true };
            return Preference;
        }

        public Preference GetEenheidPreference(EducationUnit eenheid, int userId)
        {
            List<Preference> preferences = new List<Preference>();

            var taken = _repo.GetTakenFromEenheid(eenheid);
            var onderdelen = _repo.GetAllOnderwijsOnderdelen(eenheid.Id);

            GetTakenPreferences(taken, preferences, userId);
            GetOnderdelenPreferences(onderdelen, preferences, userId);

            var Preference = new Preference {Value = CalcAveragePreference(preferences), Task = eenheid, ValueIsAverage = true };
            return Preference;
        }

        public Preference GetOnderdeelPreference(OnderwijsOnderdeel onderdeel, int userId)
        {
            List<Preference> preferences = new List<Preference>();
            Preference Preference;

            var taken = _repo.GetAllOnderwijsTaken(onderdeel.Id);
            if (taken.Count() == 0)
            {
                Preference = CheckOnderdeelPreference(onderdeel, userId);
            }
            else
            {
                GetTakenPreferences(taken, preferences, userId);
                Preference = new Preference { Value = CalcAveragePreference(preferences), Task = onderdeel, ValueIsAverage  = true};
            }
            return Preference;
        }

        public Preference GetTaskPreference(OnderwijsTask Task, int userId)
        {
            var Preference = new Preference{Value = _repo.CheckTaskPreference(Task, userId).Value, Task = Task};
            return Preference;
        }

        public List<Preference> GetOnderdelenPreferences(IEnumerable<OnderwijsOnderdeel> onderdelen, List<Preference> preferences, int userId)
        {
            foreach (var onderdeel in onderdelen)
            {
                var value = _repo.CheckOnderdeelPreference(onderdeel, userId);
                if (value.Value > 0)
                {
                    preferences.Add(value);
                }
            }
            return preferences;
        }

        public List<Preference> GetTakenPreferences(IEnumerable<OnderwijsTask> taken, List<Preference> preferences, int userId)
        {
            foreach (var Task in taken)
            {
                var value = _repo.CheckTaskPreference(Task, userId);
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

        public void SaveTrajectPreferences(List<Preference> preferences, int userId)
        {
            _repo.SaveTrajectPreferences(preferences, userId);
        }

        public Preference CheckTaskPreference(OnderwijsTask Task, int userId)
        {
            return _repo.CheckTaskPreference(Task, userId);
        }

        public void AddTaskPreference(OnderwijsTask Task, int PreferenceValue, int userId)
        {
            _repo.AddTaskPreference(Task, PreferenceValue, userId);
        }

        public void UpdateTaskPreference(OnderwijsTask Task, int PreferenceValue, int userId)
        {
            _repo.UpdateTaskPreference(Task, PreferenceValue, userId);
        }

        public Preference CheckOnderdeelPreference(OnderwijsOnderdeel onderdeel, int userId)
        {
            return _repo.CheckOnderdeelPreference(onderdeel, userId);
        }

        public void AddOnderdeelPreference(OnderwijsOnderdeel onderdeel, int PreferenceValue, int userId)
        {
            _repo.AddOnderdeelPreference(onderdeel, PreferenceValue, userId);
        }

        public void UpdateOnderdeelPreference(OnderwijsOnderdeel onderdeel, int PreferenceValue, int userId)
        {
            _repo.UpdateOnderdeelPreference(onderdeel, PreferenceValue, userId);
        }

        public IEnumerable<EducationUnit> GetAllOnderwijsEenheden(int trajectId)
        {
            return _repo.GetAllOnderwijsEenheden(trajectId);
        }

        public IEnumerable<OnderwijsOnderdeel> GetAllOnderwijsOnderdelen(int eenheidId)
        {
            return _repo.GetAllOnderwijsOnderdelen(eenheidId);
        }

        public IEnumerable<OnderwijsTask> GetAllOnderwijsTaken(int onderdeelId)
        {
            return _repo.GetAllOnderwijsTaken(onderdeelId);
        }

        public IEnumerable<OnderwijsTask> GetTasksFromTraject(EducationSection onderwijsTraject)
        {
            return _repo.GetTakenFromTraject(onderwijsTraject);
        }

        public IEnumerable<OnderwijsOnderdeel> GetOnderdeelFromTraject(EducationSection onderwijsTraject)
        {
            return _repo.GetOnderdeelFromTraject(onderwijsTraject);
        }

        public IEnumerable<OnderwijsTask> GetTasksFromEenheid(EducationUnit onderwijsEenheid)
        {
            return _repo.GetTakenFromEenheid(onderwijsEenheid);
        }
    }
}
