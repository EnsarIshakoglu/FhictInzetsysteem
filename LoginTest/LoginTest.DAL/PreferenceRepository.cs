using System;
using System.Collections.Generic;
using System.Text;
using FHICTDeploymentSystem.DAL.Contexts;
using FHICTDeploymentSystem.Models;

namespace FHICTDeploymentSystem.DAL
{
    public class PreferenceRepository
    {
        private readonly IPreferenceContext _context;
        public PreferenceRepository()
        {
            _context = new PreferenceContext();
        }

        public IEnumerable<EducationObject> GetAllSections()
        {
            return _context.GetAllSections();
        }

        public void AddTaskPreference(EducationObject task, int priority, int userId)
        {
            _context.AddTaskPreference(task, priority, userId);
        }

        public void UpdateTaskPreference(EducationObject task, int priority, int userId)
        {
            _context.UpdateTaskPreference(task, priority, userId);
        }

        public Preference CheckTaskPreference(EducationObject task, int userId)
        {
            return _context.CheckTaskPreference(task, userId);
        }

        public IEnumerable<EducationObject> GetAllUnits(int SectionId)
        {
            return _context.GetAllUnits(SectionId);
        }

        public IEnumerable<EducationObject> GetAllTasks(int UnitId)
        {
            return _context.GetAllTasks(UnitId);
        }

        public IEnumerable<EducationObject> GetTasksFromSection(EducationObject Section)
        {
            return _context.GetTasksFromSection(Section);
        }
    }
}
