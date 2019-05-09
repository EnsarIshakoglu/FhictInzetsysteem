using System.Collections.Generic;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    public interface IPreferenceContext
    {
        IEnumerable<EducationSection> GetAllEducationSectionen();
        void SaveTrajectPreferences(IEnumerable<Preference> preferences, int userId);
        void AddTaskPreference(Task task, int priority, int userId);
        void UpdateTaskPreference(Task task, int priority, int userId);
        Preference CheckTaskPreference(Task task, int userId);
        IEnumerable<EducationUnit> GetAllOnderwijsEenheden(int trajectId);
        IEnumerable<Task> GetAllTasks(int EdUnitId);
        IEnumerable<Task> GetTakenFromTraject(EducationSection EducationSection);

    }
}