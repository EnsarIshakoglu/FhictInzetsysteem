using System;
using System.Collections.Generic;
using System.Linq;
using Inzetsysteem.Logic;
using Inzetsysteem.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inzetsysteem.Controllers
{
    public class PreferenceController : Controller
    {
        private readonly PreferenceLogic _preferenceLogic = new PreferenceLogic();


        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult OnderwijsTrajectPreference()
        {
            var preferences = new List<Preference>();
            //var trajecten = _preferenceLogic.GetAllOnderwijsTrajecten();
            var trajecten = new List<Task>
            {
                new OnderwijsTraject
                {
                    Naam = "Software",
                    Id = 1
                },
                new OnderwijsTraject
                {
                    Naam = "Technology",
                    Id = 2
                },
                new OnderwijsTraject
                {
                    Naam = "Media",
                    Id = 3
                },
            };

            foreach (var onderwijsTraject in trajecten)
            {
                preferences.Add(new Preference {
                    Taak = onderwijsTraject,
                    Waarde = 3,
                    WaardeIsAverage = true
                });
            }

            return View("SubmitPreferences", preferences);
        }

        [HttpPost]
        public IActionResult GetTrajectPreferences()
        {
            List<Preference> preferences = new List<Preference>();

            foreach (var traject in _preferenceLogic.GetAllOnderwijsTrajecten())
            {
                var preferenceValue = Request.Form[traject.Naam].ToString();
                int value = Convert.ToInt16(preferenceValue);                               //todo convert.toint vervangen met iets netters

                preferences.Add(new Preference { Taak = traject, Waarde = value });
            }

            _preferenceLogic.SaveTrajectPreferences(preferences, Convert.ToInt32(User.Identity.Name));

            return RedirectToAction("OnderwijsTrajectPreference", "Preference");
        }

        [HttpPost]
        public IActionResult OnderwijsEenheidPreference(int trajectId)
        {
            var preferences = new List<Preference>();
            //var eenheden = _preferenceLogic.GetAllOnderwijsEenheden(trajectId);
            var eenheden = new List<Task>
            {
                new OnderwijsEenheid()
                {
                    Naam = "aaaaaa",
                    Id = 1
                },
                new OnderwijsEenheid()
                {
                    Naam = "bbbbbbb",
                    Id = 2
                },
                new OnderwijsEenheid()
                {
                    Naam = "ccccc",
                    Id = 3
                },
            };

            foreach (var onderwijsEenheid in eenheden)
            {
                preferences.Add(new Preference
                {
                    Taak = onderwijsEenheid,
                    Waarde = 3,
                    WaardeIsAverage = true
                });
            }

            return View("SubmitPreferences", preferences);
        }
        [HttpPost]
        public IActionResult OnderwijsTaakPreferenceView(int trajectId)
        {
            var preferences = new List<Preference>();
            //var eenheden = _preferenceLogic.GetAllOnderwijsEenheden(trajectId);
            var eenheden = new List<Task>
            {
                new OnderwijsTaak
                {
                    Naam = "taak1",
                    Id = 1,
                },
                new OnderwijsTaak
                {
                    Naam = "taak2",
                    Id = 2
                },
                new OnderwijsTaak
                {
                    Naam = "taak3",
                    Id = 3
                },
            };

            foreach (var onderwijsEenheid in eenheden)
            {
                preferences.Add(new Preference
                {
                    Taak = onderwijsEenheid,
                    Waarde = 3,
                    WaardeIsAverage = false
                });
            }

            return View("SubmitPreferences", preferences);
        }
        [HttpPost]
        public IActionResult OnderwijsOnderdeelPreference(int eenheidId)
        {
            var preferences = new List<Preference>();
            var onderdelen = _preferenceLogic.GetAllOnderwijsOnderdelen(eenheidId);

            foreach (var onderwijsOnderdeel in onderdelen)
            {
                preferences.Add(_preferenceLogic.GetOnderdeelPreference(onderwijsOnderdeel, Convert.ToInt32(User.Identity.Name)));
            }

            return View("SubmitPreferences", preferences);
        }
        [HttpPost]
        public IActionResult OnderwijsTaakPreference(int onderdeelId)
        {
            var preferences = new List<Preference>();
            var taken = _preferenceLogic.GetAllOnderwijsTaken(onderdeelId);

            foreach (var onderwijsTaak in taken)
            {
                preferences.Add(_preferenceLogic.GetTaakPreference(onderwijsTaak, Convert.ToInt32(User.Identity.Name)));
            }

            return View("SubmitPreferences", preferences);
        }

        [HttpPost]
        public IActionResult GetEenheidPreferences()
        {
            List<Preference> preferences = new List<Preference>();

            foreach (var traject in _preferenceLogic.GetAllOnderwijsTrajecten())
            {
                var preferenceValue = Request.Form[traject.Naam].ToString();
                int value = Convert.ToInt16(preferenceValue);                               //todo convert.toint vervangen met iets netters

                preferences.Add(new Preference { Taak = traject, Waarde = value });
            }

            _preferenceLogic.SaveTrajectPreferences(preferences, Convert.ToInt32(User.Identity.Name));

            return RedirectToAction("OnderwijsEenheidPreference", "Preference");
        }

        public IActionResult RedirectLayer(string taakNaam, int id)
        {
            if (taakNaam == typeof(OnderwijsTraject).Name)
            {
                return OnderwijsEenheidPreference(id);
            }
            else if (taakNaam == typeof(OnderwijsEenheid).Name)
            {
                return OnderwijsTaakPreferenceView(id);
            }
            else if (taakNaam == typeof(OnderwijsOnderdeel).Name)
            {
                return OnderwijsTaakPreference(id);
            }
            else
            {
                return null;
            }
        }
    }
}