using System;
using System.Collections.Generic;
using System.Text;
using Inzetsysteem.DAL.Contexts;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL
{
    public class PreferenceRepository
    {
        private readonly IPreferenceContext _context;
        public PreferenceRepository()
        {
            _context = new PreferenceContext();
        }

        public IEnumerable<OnderwijsTraject> GetAllOnderwijsTrajecten()
        {
            return _context.GetAllOnderwijsTrajecten();
        }

        public IEnumerable<Preference> GetAllTrajectPreferences(OnderwijsTraject traject, int IdUser)
        {
            return _context.GetPreferencesFromTraject(traject, IdUser);
        }

        public void AddTaakPreference(OnderwijsTaak taak, int voorkeurWaarde, int userId)
        {
            _context.AddTaakPreference(taak, voorkeurWaarde, userId);
        }

        public void UpdateTaakPreference(OnderwijsTaak taak, int voorkeurWaarde, int userId)
        {
            _context.UpdateTaakPreference(taak,voorkeurWaarde,userId);
        }

        public Preference CheckOnderdeelPreference(OnderwijsOnderdeel onderdeel, int userId)
        {
            return _context.CheckOnderdeelPreference(onderdeel, userId);
        }

        public void AddOnderdeelPreference(OnderwijsOnderdeel onderdeel, int voorkeurWaarde, int userId)
        {
            _context.AddOnderdeelPreference(onderdeel, voorkeurWaarde, userId);
        }

        public void UpdateOnderdeelPreference(OnderwijsOnderdeel onderdeel, int voorkeurWaarde, int userId)
        {
            _context.UpdateOnderdeelPreference(onderdeel, voorkeurWaarde, userId);
        }

        public Preference CheckTaakPreference(OnderwijsTaak taak, int userId)
        {
            return _context.CheckTaakPreference(taak, userId);
        }

        public void SaveTrajectPreferences(IEnumerable<Preference> preferences, int userId)
        {
            _context.SaveTrajectPreferences(preferences, userId);
        }
    }
}
