using System.Collections.Generic;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    public interface IPreferenceContext
    {
        IEnumerable<OnderwijsTraject> GetAllOnderwijsTrajecten();
        void SaveTrajectPreferences(IEnumerable<Preference> preferences, int userId);
        void AddTaskPreference(OnderwijsTaak task, int priority, int userId);
        void UpdateTaskPreference(OnderwijsTaak task, int priority, int userId);
        Preference CheckTaskPreference(OnderwijsTaak task, int userId);
        IEnumerable<OnderwijsEenheid> GetAllOnderwijsEenheden(int trajectId);
        IEnumerable<OnderwijsTaak> GetAllTasks(int EdUnitId);
        IEnumerable<OnderwijsTaak> GetTakenFromTraject(OnderwijsTraject onderwijsTraject);

    }
}