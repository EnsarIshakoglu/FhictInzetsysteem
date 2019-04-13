using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    class OnderwijsRepo : IOnderwijsTrajectContext
    {
        private readonly OnderwijsTrajectContext onderwijsTrajectContext;

        public List<OnderwijsTraject> GetAllTrajects()
        {
            return onderwijsTrajectContext.GetAllTrajects();
        }

        public OnderwijsTraject GetOnderwijsTrajectById(int id)
        {
            throw new NotImplementedException();
        }

        public OnderwijsTraject GetTrajectsPerTeam(Team team)
        {
            throw new NotImplementedException();
        }
    }
}