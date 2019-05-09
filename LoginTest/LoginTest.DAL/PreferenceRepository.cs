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

        public IEnumerable<Section> GetAllSections()
        {
            return _context.GetAllSections();
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

        public void SaveEdSectionPreferences(IEnumerable<Preference> preferences, int userId)
        {
            _context.SaveEdSectionPreferences(preferences, userId);
        }

        public IEnumerable<Unit> GetAllUnits(int EdSectionId)
        {
            return _context.GetAllUnits(EdSectionId);
        }

        public IEnumerable<Task> GetAllTasks(int EdUnitId)
        {
            return _context.GetAllTasks(EdUnitId);
        }

        public IEnumerable<Task> GetTasksFromEdSection(Section edSection)
        {
            return _context.GetTasksFromEdSection(edSection);
        }
    }
}
