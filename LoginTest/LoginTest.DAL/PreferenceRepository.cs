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

        public IEnumerable<EducationSection> GetAllEducationSectionen()
        {
            return _context.GetAllEducationSectionen();
        }

        public void AddTaskPreference(Task task, int priority, int userId)
        {
            _context.AddTaskPreference(task, priority, userId);
        }

        public void UpdateTaskPreference(Task task, int priority, int userId)
        {
            _context.UpdateTaskPreference(task, priority, userId);
        }

        public Preference CheckTaskPreference(Task task, int userId)
        {
            return _context.CheckTaskPreference(task, userId);
        }

        public void SaveTrajectPreferences(IEnumerable<Preference> preferences, int userId)
        {
            _context.SaveTrajectPreferences(preferences, userId);
        }

        public IEnumerable<EducationUnit> GetAllOnderwijsEenheden(int trajectId)
        {
            return _context.GetAllOnderwijsEenheden(trajectId);
        }

        public IEnumerable<Task> GetAllTasks(int EdUnitId)
        {
            return _context.GetAllTasks(EdUnitId);
        }

        public IEnumerable<Task> GetTakenFromTraject(EducationSection EducationSection)
        {
            return _context.GetTakenFromTraject(EducationSection);
        }
    }
}
