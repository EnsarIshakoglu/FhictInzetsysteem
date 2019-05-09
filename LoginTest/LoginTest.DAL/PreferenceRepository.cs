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

        public IEnumerable<EducationSection> GetAllOnderwijsTrajecten()
        {
            return _context.GetAllOnderwijsTrajecten();
        }

        public void AddTaskPreference(OnderwijsTask Task, int PreferenceValue, int userId)
        {
            _context.AddTaskPreference(Task, PreferenceValue, userId);
        }

        public void UpdateTaskPreference(OnderwijsTask Task, int PreferenceValue, int userId)
        {
            _context.UpdateTaskPreference(Task,PreferenceValue,userId);
        }

        public Preference CheckOnderdeelPreference(OnderwijsOnderdeel onderdeel, int userId)
        {
            return _context.CheckOnderdeelPreference(onderdeel, userId);
        }

        public void AddOnderdeelPreference(OnderwijsOnderdeel onderdeel, int PreferenceValue, int userId)
        {
            _context.AddOnderdeelPreference(onderdeel, PreferenceValue, userId);
        }

        public void UpdateOnderdeelPreference(OnderwijsOnderdeel onderdeel, int PreferenceValue, int userId)
        {
            _context.UpdateOnderdeelPreference(onderdeel, PreferenceValue, userId);
        }

        public Preference CheckTaskPreference(OnderwijsTask Task, int userId)
        {
            return _context.CheckTaskPreference(Task, userId);
        }

        public void SaveTrajectPreferences(IEnumerable<Preference> preferences, int userId)
        {
            _context.SaveTrajectPreferences(preferences, userId);
        }

        public IEnumerable<EducationUnit> GetAllOnderwijsEenheden(int trajectId)
        {
            return _context.GetAllOnderwijsEenheden(trajectId);
        }

        public IEnumerable<OnderwijsOnderdeel> GetAllOnderwijsOnderdelen(int eenheidId)
        {
            return _context.GetAllOnderwijsOnderdelen(eenheidId);
        }

        public IEnumerable<OnderwijsTask> GetAllOnderwijsTaken(int onderdeelId)
        {
            return _context.GetAllOnderwijsTaken(onderdeelId);
        }

        public IEnumerable<OnderwijsTask> GetTakenFromTraject(EducationSection onderwijsTraject)
        {
            return _context.GetTakenFromTraject(onderwijsTraject);
        }

        public IEnumerable<OnderwijsOnderdeel> GetOnderdeelFromTraject(EducationSection onderwijsTraject)
        {
            return _context.GetOnderdeelFromTraject(onderwijsTraject);
        }

        public IEnumerable<OnderwijsTask> GetTakenFromEenheid(EducationUnit onderwijsEenheid)
        {
            return _context.GetTakenFromEenheid(onderwijsEenheid);
        }
    }
}
