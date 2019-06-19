using System.Collections.Generic;
using FHICTDeploymentSystem.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace FHICTDeploymentSystem.Controllers
{
    public class TermExecController : Controller
    {
        private readonly PreferenceLogic _preferenceLogic = new PreferenceLogic();
        private readonly TermExecLogic _termExecLogic = new TermExecLogic();

        public IActionResult AddTermExec()
        {
            var viewModel = new AddUnitTermExecViewModel
            {
                Sections = _preferenceLogic.GetAllSections(),
                TermExecs = _termExecLogic.GetAllTermExecs()
            };

            return View(viewModel);
        }

        [HttpPost]
        public void AddTermExec([FromBody] EducationObject termExecToAdd)
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