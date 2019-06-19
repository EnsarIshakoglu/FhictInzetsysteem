using System;
using System.Collections.Generic;
using System.Text;
using DAL.Contexts;
using Models;

namespace DAL
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

        public IEnumerable<EducationObject> GetAllTasks(int unitExecId)
        {
            return _context.GetAllTasks(unitExecId);
        }

        public IEnumerable<EducationObject> GetTasksFromSection(EducationObject Section)
        {
            return _context.GetTasksFromSection(Section);
        }

        public IEnumerable<EducationObject> GetTasksFromUnit(int unitId)
        {
            return _context.GetTasksFromUnit(unitId);
        }

        public IEnumerable<EducationObject> GetTasksFromUnitExecution(int unitExecutionId)
        {
            return _context.GetTasksFromUnitExecution(unitExecutionId);
        }
    }
}
