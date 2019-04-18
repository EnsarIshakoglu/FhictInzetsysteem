using System.Collections.Generic;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    public interface IPreferenceContext
    {
        IEnumerable<OnderwijsTraject> GetAllOnderwijsTrajecten();
        IEnumerable<Preference> GetPreferencesFromTraject(OnderwijsTraject traject, int userId);
        void SaveTrajectPreferences(IEnumerable<Preference> preferences, int userId);
        void AddTaakPreference(OnderwijsTaak taak, int voorkeurWaarde, int userId);
        void UpdateTaakPreference(OnderwijsTaak taak, int voorkeurWaarde, int userId);
        Preference CheckOnderdeelPreference(OnderwijsOnderdeel onderdeel, int userId);
        void AddOnderdeelPreference(OnderwijsOnderdeel onderdeel, int voorkeurWaarde, int userId);
        void UpdateOnderdeelPreference(OnderwijsOnderdeel onderdeel, int voorkeurWaarde, int userId);
        Preference CheckTaakPreference(OnderwijsTaak taak, int userId);

    }
}