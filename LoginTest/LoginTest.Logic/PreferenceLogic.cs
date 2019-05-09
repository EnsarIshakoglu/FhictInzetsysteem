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

            var taken = _repo.GetTakenFromTraject(traject);
            var onderdelen = _repo.GetOnderdeelFromTraject(traject);

            GetTakenPreferences(taken, preferences, userId);
            GetOnderdelenPreferences(onderdelen, preferences, userId);
            var voorkeur = new Preference { Waarde = CalcAveragePreference(preferences), Taak = traject, WaardeIsAverage = true };
            return voorkeur;
        }

        public Preference GetEenheidPreference(OnderwijsEenheid eenheid, int userId)
        {
            List<Preference> preferences = new List<Preference>();

            var taken = _repo.GetTakenFromEenheid(eenheid);
            var onderdelen = _repo.GetAllOnderwijsOnderdelen(eenheid.Id);

            GetTakenPreferences(taken, preferences, userId);
            GetOnderdelenPreferences(onderdelen, preferences, userId);

            var voorkeur = new Preference {Waarde = CalcAveragePreference(preferences), Taak = eenheid, WaardeIsAverage = true };
            return voorkeur;
        }

        public Preference GetOnderdeelPreference(OnderwijsOnderdeel onderdeel, int userId)
        {
            List<Preference> preferences = new List<Preference>();
            Preference voorkeur;

            var taken = _repo.GetAllOnderwijsTaken(onderdeel.Id);
            if (taken.Count() == 0)
            {
                voorkeur = CheckOnderdeelPreference(onderdeel, userId);
            }
            else
            {
                GetTakenPreferences(taken, preferences, userId);
                voorkeur = new Preference { Waarde = CalcAveragePreference(preferences), Taak = onderdeel, WaardeIsAverage  = true};
            }
            return voorkeur;
        }

        public Preference GetTaakPreference(OnderwijsTaak taak, int userId)
        {
            var voorkeur = new Preference{Waarde = _repo.CheckTaakPreference(taak, userId).Waarde, Taak = taak};
            return voorkeur;
        }

        public List<Preference> GetOnderdelenPreferences(IEnumerable<OnderwijsOnderdeel> onderdelen, List<Preference> preferences, int userId)
        {
            foreach (var onderdeel in onderdelen)
            {
                var value = _repo.CheckOnderdeelPreference(onderdeel, userId);
                if (value.Waarde > 0)
                {
                    preferences.Add(value);
                }
            }
            return preferences;
        }

        public List<Preference> GetTakenPreferences(IEnumerable<OnderwijsTaak> taken, List<Preference> preferences, int userId)
        {
            foreach (var taak in taken)
            {
                var value = _repo.CheckTaakPreference(taak, userId);
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

        public Preference CheckTaakPreference(OnderwijsTaak taak, int userId)
        {
            return _repo.CheckTaakPreference(taak, userId);
        }

        public void AddTaakPreference(OnderwijsTaak taak, int voorkeurWaarde, int userId)
        {
            _repo.AddTaakPreference(taak, voorkeurWaarde, userId);
        }

        public void UpdateTaakPreference(OnderwijsTaak taak, int voorkeurWaarde, int userId)
        {
            _repo.UpdateTaakPreference(taak, voorkeurWaarde, userId);
        }

        public Preference CheckOnderdeelPreference(OnderwijsOnderdeel onderdeel, int userId)
        {
            return _repo.CheckOnderdeelPreference(onderdeel, userId);
        }

        public void AddOnderdeelPreference(OnderwijsOnderdeel onderdeel, int voorkeurWaarde, int userId)
        {
            _repo.AddOnderdeelPreference(onderdeel, voorkeurWaarde, userId);
        }

        public void UpdateOnderdeelPreference(OnderwijsOnderdeel onderdeel, int voorkeurWaarde, int userId)
        {
            _repo.UpdateOnderdeelPreference(onderdeel, voorkeurWaarde, userId);
        }

        public IEnumerable<OnderwijsEenheid> GetAllOnderwijsEenheden(int trajectId)
        {
            return _repo.GetAllOnderwijsEenheden(trajectId);
        }

        public IEnumerable<OnderwijsOnderdeel> GetAllOnderwijsOnderdelen(int eenheidId)
        {
            return _repo.GetAllOnderwijsOnderdelen(eenheidId);
        }

        public IEnumerable<OnderwijsTaak> GetAllOnderwijsTaken(int onderdeelId)
        {
            return _repo.GetAllOnderwijsTaken(onderdeelId);
        }

        public IEnumerable<OnderwijsTaak> GetTasksFromTraject(OnderwijsTraject onderwijsTraject)
        {
            return _repo.GetTakenFromTraject(onderwijsTraject);
        }

        public IEnumerable<OnderwijsOnderdeel> GetOnderdeelFromTraject(OnderwijsTraject onderwijsTraject)
        {
            return _repo.GetOnderdeelFromTraject(onderwijsTraject);
        }

        public IEnumerable<OnderwijsTaak> GetTasksFromEenheid(OnderwijsEenheid onderwijsEenheid)
        {
            return _repo.GetTakenFromEenheid(onderwijsEenheid);
        }
    }
}
