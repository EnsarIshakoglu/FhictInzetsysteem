using System.Collections.Generic;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    public interface IPreferenceContext
    {
        IEnumerable<OnderwijsTraject> GetAllOnderwijsTrajecten();
        IEnumerable<Voorkeur> GetPreferencesFromTraject(OnderwijsTraject traject, int IdUser);
    }
}