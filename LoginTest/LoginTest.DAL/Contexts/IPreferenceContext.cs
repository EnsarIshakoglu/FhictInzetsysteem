using System.Collections.Generic;
using FHICTDeploymentSystem.Models;

namespace FHICTDeploymentSystem.DAL.Contexts
{
    public interface IPreferenceContext
    {
        IEnumerable<Section> GetAllSections();
        void AddTaskPreference(Task task, int priority, int userId);
        void UpdateTaskPreference(Task task, int priority, int userId);
        Preference CheckTaskPreference(Task task, int userId);
        IEnumerable<Unit> GetAllUnits(int SectionId);
        IEnumerable<Task> GetAllTasks(int UnitId);
        IEnumerable<Task> GetTasksFromSection(Section Section);

    }
}