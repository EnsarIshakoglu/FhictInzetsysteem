using System.Collections.Generic;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    public interface IPreferenceContext
    {
        IEnumerable<EducationSection> GetAllEducationSections();
        void SaveEdSectionPreferences(IEnumerable<Preference> preferences, int userId);
        void AddTaskPreference(Task task, int priority, int userId);
        void UpdateTaskPreference(Task task, int priority, int userId);
        Preference CheckTaskPreference(Task task, int userId);
        IEnumerable<EducationUnit> GetAllEducationUnits(int EdSectionId);
        IEnumerable<Task> GetAllTasks(int EdUnitId);
        IEnumerable<Task> GetTasksFromEdSection(EducationSection EducationSection);

    }
}