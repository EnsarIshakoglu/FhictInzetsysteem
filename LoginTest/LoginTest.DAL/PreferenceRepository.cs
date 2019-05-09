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

        public IEnumerable<OnderwijsEenheid> GetAllOnderwijsEenheden(int trajectId)
        {
            return _context.GetAllOnderwijsEenheden(trajectId);
        }

        public IEnumerable<OnderwijsOnderdeel> GetAllOnderwijsOnderdelen(int eenheidId)
        {
            return _context.GetAllOnderwijsOnderdelen(eenheidId);
        }

        public IEnumerable<OnderwijsTaak> GetAllOnderwijsTaken(int onderdeelId)
        {
            return _context.GetAllOnderwijsTaken(onderdeelId);
        }

        public IEnumerable<OnderwijsTaak> GetTakenFromTraject(OnderwijsTraject onderwijsTraject)
        {
            return _context.GetTakenFromTraject(onderwijsTraject);
        }

        public IEnumerable<OnderwijsOnderdeel> GetOnderdeelFromTraject(OnderwijsTraject onderwijsTraject)
        {
            return _context.GetOnderdeelFromTraject(onderwijsTraject);
        }

        public IEnumerable<OnderwijsTaak> GetTakenFromEenheid(OnderwijsEenheid onderwijsEenheid)
        {
            return _context.GetTakenFromEenheid(onderwijsEenheid);
        }
    }
}
