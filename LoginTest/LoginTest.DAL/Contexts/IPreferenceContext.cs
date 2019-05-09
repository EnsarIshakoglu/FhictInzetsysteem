using System.Collections.Generic;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    public interface IPreferenceContext
    {
        IEnumerable<EducationSection> GetAllOnderwijsTrajecten();
        void SaveTrajectPreferences(IEnumerable<Preference> preferences, int userId);
        void AddTaskPreference(OnderwijsTask Task, int PreferenceValue, int userId);
        void UpdateTaskPreference(OnderwijsTask Task, int PreferenceValue, int userId);
        Preference CheckOnderdeelPreference(OnderwijsOnderdeel onderdeel, int userId);
        void AddOnderdeelPreference(OnderwijsOnderdeel onderdeel, int PreferenceValue, int userId);
        void UpdateOnderdeelPreference(OnderwijsOnderdeel onderdeel, int PreferenceValue, int userId);
        Preference CheckTaskPreference(OnderwijsTask Task, int userId);
        IEnumerable<EducationUnit> GetAllOnderwijsEenheden(int trajectId);
        IEnumerable<OnderwijsOnderdeel> GetAllOnderwijsOnderdelen(int EenheidId);
        IEnumerable<OnderwijsTask> GetAllOnderwijsTaken(int OnderdeelId);
        IEnumerable<OnderwijsTask> GetTakenFromTraject(EducationSection onderwijsTraject);
        IEnumerable<OnderwijsOnderdeel> GetOnderdeelFromTraject(EducationSection onderwijsTraject);
        IEnumerable<OnderwijsTask> GetTakenFromEenheid(EducationUnit onderwijsEenheid);

    }
}