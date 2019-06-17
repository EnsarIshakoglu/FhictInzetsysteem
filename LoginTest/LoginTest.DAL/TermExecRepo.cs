using System;
using System.Collections.Generic;
using System.Text;
using DAL.Contexts;
using FHICTDeploymentSystem.Models;

namespace DAL
{
    public class TermExecRepo
    {
        private readonly ITermExecContext _context;

        public TermExecRepo()
        {
            _context = new TermExecContext();
        }

        public void AddTermExec(EducationObject termExec)
        {
            _context.AddTermExec(termExec);
        }
        public void DeleteTermExec(EducationObject termExec)
        {
            _context.DeleteTermExec(termExec);
        }
        public void EditTermExec(EducationObject termExec)
        {
            _context.EditTermExec(termExec);
        }
    }
}
