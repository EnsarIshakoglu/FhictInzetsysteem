using System.Collections.Generic;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    public interface IPreferenceContext
    {
        IEnumerable<OnderwijsTraject> GetAllOnderwijsTrajecten();
        void SaveTrajectPreferences(IEnumerable<Preference> preferences, int userId);
        void AddTaakPreference(OnderwijsTaak taak, int voorkeurWaarde, int userId);
        void UpdateTaakPreference(OnderwijsTaak taak, int voorkeurWaarde, int userId);
        Preference CheckOnderdeelPreference(OnderwijsOnderdeel onderdeel, int userId);
        void AddOnderdeelPreference(OnderwijsOnderdeel onderdeel, int voorkeurWaarde, int userId);
        void UpdateOnderdeelPreference(OnderwijsOnderdeel onderdeel, int voorkeurWaarde, int userId);
        Preference CheckTaakPreference(OnderwijsTaak taak, int userId);
        IEnumerable<OnderwijsEenheid> GetAllOnderwijsEenheden(int trajectId);
        IEnumerable<OnderwijsOnderdeel> GetAllOnderwijsOnderdelen(int EenheidId);
        IEnumerable<OnderwijsTaak> GetAllOnderwijsTaken(int OnderdeelId);
        IEnumerable<OnderwijsTaak> GetTakenFromTraject(OnderwijsTraject onderwijsTraject);
        IEnumerable<OnderwijsOnderdeel> GetOnderdeelFromTraject(OnderwijsTraject onderwijsTraject);
        IEnumerable<OnderwijsTaak> GetTakenFromEenheid(OnderwijsEenheid onderwijsEenheid);

    }
}