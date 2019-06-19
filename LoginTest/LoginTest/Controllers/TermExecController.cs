using System.Collections.Generic;
using FHICTDeploymentSystem.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;

namespace FHICTDeploymentSystem.Controllers
{
    public class TermExecController : Controller
    {
        private readonly PreferenceLogic _preferenceLogic = new PreferenceLogic();
        private readonly TermExecLogic _termExecLogic = new TermExecLogic();

        public IActionResult AddUnitTermExec()
        {
            var viewModel = new ManageUnitTermExecViewModel
            {
                Sections = _preferenceLogic.GetAllSections(),
                TermExecs = _termExecLogic.GetAllTermExecs()
            };

            return View(viewModel);
        }

        public IActionResult UnitTermExecSelectorForEdit()
        {
            var viewModel = new ManageUnitTermExecViewModel
            {
                Sections = _preferenceLogic.GetAllSections(),
                TermExecs = _termExecLogic.GetAllTermExecs()
            };

            return View(viewModel);
        }

        public IActionResult EditUnitTermExec(int unitTermExecId)
        {
            var unitTerm = _termExecLogic.GetUnitTermExecFromId(unitTermExecId);

            return View("EditUnitTermExec",unitTerm);
        }

        [HttpPost]
        public void AddUnitTermExec([FromBody] EducationObject termExecToAdd)
        {
            _termExecLogic.AddTermExec(termExecToAdd);
        }
        [HttpPost]
        public void EditTermExec([FromBody] EducationObject termExecToEdit)
        {
            _termExecLogic.EditTermExec(termExecToEdit);
        }
        [HttpPost]
        public void DeleteTermExec([FromBody] EducationObject termExecToDelete)
        {
            _termExecLogic.DeleteTermExec(termExecToDelete);
        }
    }
}