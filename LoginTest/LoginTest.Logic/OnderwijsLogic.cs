using System;
using System.Collections.Generic;
using System.Text;
using Inzetsysteem.DAL.Contexts;
using Inzetsysteem.Models;

namespace Inzetsysteem.Logic
{
    public class OnderwijsLogic
    {
        private readonly OnderwijsRepo onderwijsRepo;

        public OnderwijsLogic()
        {
            onderwijsRepo = new OnderwijsRepo();
        }

        public List<OnderwijsTraject> GetAllTrajects()
        {
            return onderwijsRepo.GetAllTrajects();
        }

        public List<OnderwijsEenheid> GetAllEenheden()
        {
            return onderwijsRepo.GetAllEenheden();
        }

        public List<OnderwijsEenheid> GetAllEenhedenByTraject(OnderwijsTraject traject)
        {
            return onderwijsRepo.GetAllEenhedenByTraject(traject);
        }

        public List<OnderwijsOnderdeel> GetAllOnderdelen()
        {
            return onderwijsRepo.GetAllOnderdelen();
        }

        public List<OnderwijsOnderdeel> GetAllOnderdelenByEenheid(OnderwijsEenheid eenheid)
        {
            return onderwijsRepo.GetAllOnderdelenByEenheid(eenheid);
        }

        public List<OnderwijsTaak> GetAllTaken()
        {
            return onderwijsRepo.GetAllTaken();
        }

        public List<OnderwijsTaak> GetAllTakenByOnderdeel(OnderwijsOnderdeel onderdeel)
        {
            return onderwijsRepo.GetAllTakenByOnderdeel(onderdeel);
        }
    }
}
