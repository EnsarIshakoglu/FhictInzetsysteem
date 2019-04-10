using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inzetsysteem.DAL;
using Inzetsysteem.Models;
using Newtonsoft.Json;

namespace Inzetsysteem.Logic
{
    public class PreferenceLogic
    {
        private readonly PreferenceRepository _repo = new PreferenceRepository();

        public IEnumerable<OnderwijsTraject> GetAllOnderwijsTrajecten()
        {
            return _repo.GetAllOnderwijsTrajecten();
        }

        public Voorkeur GetTrajectPreference(OnderwijsTraject traject, string userId)
        {
            int IdUser = Convert.ToInt32(userId);

            int preferenceValue = 0;
            var preferences = _repo.GetAllTrajectPreferences(traject, IdUser);

            int valueToDivideBy = preferences.Count();
            if (valueToDivideBy == 0) valueToDivideBy = 1;

            foreach (var preference in preferences)
            {
                preferenceValue += preference.Waarde;
            }

            var voorkeur = new Voorkeur(traject, (preferenceValue / valueToDivideBy));

            return voorkeur;
        }
    }
}
