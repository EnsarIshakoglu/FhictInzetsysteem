using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Inzetsysteem.DAL.Contexts.TaskUnits;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    public class OnderwijsRepo : IOnderwijsTrajectContext,IOnderwijsEenheidContext,IOnderwijsOnderdeelContext, IOnderwijsTaakContext
    {
        private readonly IOnderwijsTrajectContext onderwijsTrajectContext;
        private readonly IOnderwijsEenheidContext onderwijsEenheidContext;
        private readonly IOnderwijsOnderdeelContext onderwijsOnderdeelContext;
        private readonly IOnderwijsTaakContext onderwijsTaakContext;

        public OnderwijsRepo()
        {
            onderwijsTrajectContext = new OnderwijsTrajectContext();
            onderwijsEenheidContext = new OnderwijsEenheidContext();
            onderwijsOnderdeelContext = new OnderwijsOnderdeelContext();
            onderwijsTaakContext = new OnderwijsTaakContext();
        }

        public List<OnderwijsTraject> GetAllTrajects()
        {
            return onderwijsTrajectContext.GetAllTrajects();
        }

        public List<OnderwijsEenheid> GetAllEenheden()
        {
            return onderwijsEenheidContext.GetAllEenheden();
        }

        public List<OnderwijsEenheid> GetAllEenhedenByTraject(OnderwijsTraject traject)
        {
            return onderwijsEenheidContext.GetAllEenhedenByTraject(traject);
        }

        public List<OnderwijsOnderdeel> GetAllOnderdelen()
        {
            return onderwijsOnderdeelContext.GetAllOnderdelen();
        }

        public List<OnderwijsOnderdeel> GetAllOnderdelenByEenheid(OnderwijsEenheid eenheid)
        {
            return GetAllOnderdelenByEenheid(eenheid);
        }

        public List<OnderwijsTaak> GetAllTaken()
        {
            return onderwijsTaakContext.GetAllTaken();
        }

        public List<OnderwijsTaak> GetAllTakenByOnderdeel(OnderwijsOnderdeel onderdeel)
        {
            return onderwijsTaakContext.GetAllTakenByOnderdeel(onderdeel);
        }
    }
}