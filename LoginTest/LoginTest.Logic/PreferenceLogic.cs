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

        public Preference GetTrajectPreference(OnderwijsTraject traject, string userId)
        {
            int idUser = Convert.ToInt32(userId);

            int preferenceValue = 0;
            List<Preference> preferences = new List<Preference>();

            var taken = _repo.GetTakenFromTraject(traject);
            var onderdelen = _repo.GetOnderdeelFromTraject(traject);

            foreach (var taak in taken)
            {
                var value = _repo.CheckTaakPreference(taak, idUser);
                if (value.Waarde > 0)
                {
                    preferences.Add(value);
                }
            }
            foreach (var onderdeel in onderdelen)
            {
                var value = _repo.CheckOnderdeelPreference(onderdeel, idUser);
                if (value.Waarde > 0)
                {
                    preferences.Add(value);
                }
            }
            int valueToDivideBy = preferences.Count();
            if (valueToDivideBy == 0) valueToDivideBy = 1;
            
            foreach (var preference in preferences)
            {
                preferenceValue += preference.Waarde;
            }
            
            var voorkeur = new Preference{Taak = traject, Waarde = preferenceValue / valueToDivideBy};
            
            return voorkeur;
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
