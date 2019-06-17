using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using FHICTDeploymentSystem.Models;

namespace Logic
{
    public class TermExecLogic
    {
        private readonly TermExecRepo _repo = new TermExecRepo();

        public void AddTermExec(EducationObject termExec)
        {
            _repo.AddTermExec(termExec);
        }
        public void DeleteTermExec(EducationObject termExec)
        {
            _repo.DeleteTermExec(termExec);
        }
        public void EditTermExec(EducationObject termExec)
        {
            _repo.EditTermExec(termExec);
        }
    }
}
