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

        public void AddTaskPreference(OnderwijsTaak task, int priority, int userId)
        {
            _context.AddTaskPreference(task, priority, userId);
        }

        public void UpdateTaskPreference(OnderwijsTaak task, int priority, int userId)
        {
            _context.UpdateTaskPreference(task, priority, userId);
        }

        public Preference CheckTaskPreference(OnderwijsTaak task, int userId)
        {
            return _context.CheckTaskPreference(task, userId);
        }

        public void SaveTrajectPreferences(IEnumerable<Preference> preferences, int userId)
        {
            _context.SaveTrajectPreferences(preferences, userId);
        }

        public IEnumerable<OnderwijsEenheid> GetAllOnderwijsEenheden(int trajectId)
        {
            return _context.GetAllOnderwijsEenheden(trajectId);
        }

        public IEnumerable<OnderwijsTaak> GetAllTasks(int EdUnitId)
        {
            return _context.GetAllTasks(EdUnitId);
        }

        public IEnumerable<OnderwijsTaak> GetTakenFromTraject(OnderwijsTraject onderwijsTraject)
        {
            return _context.GetTakenFromTraject(onderwijsTraject);
        }
    }
}
