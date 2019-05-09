using System.Collections.Generic;
using FHICTDeploymentSystem.Models;

namespace FHICTDeploymentSystem.DAL.Contexts
{
    public interface IPreferenceContext
    {
        IEnumerable<Section> GetAllSections();
        void SaveEdSectionPreferences(IEnumerable<Preference> preferences, int userId);
        void AddTaskPreference(Task task, int priority, int userId);
        void UpdateTaskPreference(Task task, int priority, int userId);
        Preference CheckTaskPreference(Task task, int userId);
        IEnumerable<Unit> GetAllUnits(int EdSectionId);
        IEnumerable<Task> GetAllTasks(int EdUnitId);
        IEnumerable<Task> GetTasksFromEdSection(Section Section);

    }
}