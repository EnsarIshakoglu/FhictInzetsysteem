using System.Collections.Generic;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    public interface IPreferenceContext
    {
        IEnumerable<Section> GetAllSections();
        void SaveEdSectionPreferences(IEnumerable<Preference> preferences, int userId);
        void AddTaskPreference(Task task, int priority, int userId);
        void UpdateTaskPreference(Task task, int priority, int userId);
        Preference CheckTaskPreference(Task task, int userId);
        IEnumerable<Unit> GetAllUnits(int SectionId);
        IEnumerable<Task> GetAllTasks(int UnitId);
        IEnumerable<Task> GetTasksFromSection(Section Section);

    }
}