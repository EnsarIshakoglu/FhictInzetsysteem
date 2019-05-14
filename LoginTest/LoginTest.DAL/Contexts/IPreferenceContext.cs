using System.Collections.Generic;
using FHICTDeploymentSystem.Models;

namespace FHICTDeploymentSystem.DAL.Contexts
{
    public interface IPreferenceContext
    {
        IEnumerable<EducationObject> GetAllSections();
        void AddTaskPreference(EducationObject task, int priority, int userId);
        void UpdateTaskPreference(EducationObject task, int priority, int userId);
        Preference CheckTaskPreference(EducationObject task, int userId);
        IEnumerable<EducationObject> GetAllUnits(int SectionId);
        IEnumerable<EducationObject> GetAllTasks(int UnitId);
        IEnumerable<EducationObject> GetTasksFromSection(EducationObject Section);

    }
}