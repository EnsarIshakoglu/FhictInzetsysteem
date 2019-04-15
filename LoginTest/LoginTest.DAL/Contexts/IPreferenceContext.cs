using System.Collections.Generic;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    public interface IPreferenceContext
    {
        IEnumerable<OnderwijsTraject> GetAllOnderwijsTrajecten();
        IEnumerable<Preference> GetPreferencesFromTraject(OnderwijsTraject traject, int userId);
        void SaveTrajectPreferences(IEnumerable<Preference> preferences, int userId);
    }
}