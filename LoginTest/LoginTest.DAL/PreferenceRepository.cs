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

        public void SaveTrajecTPreferences(List<Preference> preferences)
        {
            return _context.SaveTrajectPreferences()
        }
    }
}
