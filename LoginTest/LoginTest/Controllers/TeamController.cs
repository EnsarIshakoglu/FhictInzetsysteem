using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FHICTDeploymentSystem.Logic;
using FHICTDeploymentSystem.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Models;
using Newtonsoft.Json;

namespace FHICTDeploymentSystem.Controllers
{
    public class TeamController : Controller
    {
        private readonly TeamLogic _teamLogic = new TeamLogic();
        private readonly PreferenceLogic _preferenceLogic = new PreferenceLogic();

        [HttpGet]
        public IActionResult ManageTeam()
        {
            User _user = new User
            {
                TeamId = 1
            };

            var userList = _teamLogic.GetTeamUsers(_user);

            return View(userList);
        }

        [HttpPost]
        public IActionResult RemoveUser([FromBody]User _user)
        {
            _teamLogic.RemoveUser(_user);
            return RedirectToAction("ManageTeam");
        }

        [HttpGet]
        public IActionResult ShowTeachers()
        {
            User _user = new User
            {
                Id = 1
            };

            var teacherList = new List<User>();
            var users = _teamLogic.GetAllUserWhitoutTeam(_user);

            foreach (var user in users)
            {
                teacherList.Add(user);
            }
            return View(teacherList);
        }

        [HttpPost]
        public IActionResult AddTeacher([FromBody]User _user)
        {
            var sid = User.Claims.First(c => c.Type.Equals(ClaimTypes.Sid)).Value;
            int.TryParse(sid, out int teamId);
            _user.TeamId = teamId;
            _teamLogic.AddTeacher(_user);
            return RedirectToAction("ManageTeam");
        }
        
        public IActionResult EditUserInTeam(User user)
        {
            var Model = new TeacherModel();
            Model.ID = user.Id;
            Model.Bewkaamheden = _teamLogic.GetTeamMemberCompetences(user);
            Model.Uren = _teamLogic.GetTeamMemberHours(user.Id);
            return View(Model);
        }

        public IActionResult SaveHours([FromBody]User user, EducationObject hours)
        {
            user.Id = 1;
            hours.EstimatedHours = 5;
            hours.EstimatedHours2 = 5;
            _teamLogic.SaveHours(user, hours);
            return View();
        }

        public IActionResult RemoveCompetence(string jsonding, int employeeId)
        {
            int[] idArray = JsonConvert.DeserializeObject<int[]>(jsonding);
            foreach (int id in idArray)
            {
                _teamLogic.RemoveCompetence(id, employeeId);
            }
            return new JsonResult(new { message = "Succes"});
        }

        public IActionResult AddSectionCompetence(int id, int employeeId)
        {
            _teamLogic.AddSectionCompetence(id, employeeId);
            return new JsonResult(new { message = "Succesfully added all tasks in section to competences" });
        }


        public IActionResult AddUnitCompetence(int id, int employeeId)
        {
            _teamLogic.AddUnitCompetence(id, employeeId);
            return new JsonResult(new { message = "Succesfully added all tasks in Unit competences" });
        }


        public IActionResult AddUnitExecCompetence(int id, int employeeId)
        {
            _teamLogic.AddUnitExecCompetence(id, employeeId);
            return new JsonResult(new { message = "Succesfully added all tasks in UnitExec competences" });
        }


        public IActionResult AddTasksCompetence(int id, int employeeId)
        {
            _teamLogic.AddTasksCompetence(id, employeeId);
            return new JsonResult(new { message = "Succesfully added task to competences" });
        }

        public IActionResult AddCompetences(int id)
        {
            User user = new User();
            
            return View(id);
        }

        public IActionResult GetSectionsWhereUserIsNotCompetentFor(int employeeId)
        {
            return Json(_teamLogic.GetSectionsWhereUserIsNotCompetentFor(employeeId));
        }

        public IActionResult GetTermExecsWhereUserIsNotCompetentFor(int employeeId,int id)
        {
            return Json(_teamLogic.GetTermExecsWhereUserIsNotCompetentFor(employeeId,id));
        }

        public IActionResult GetUnitWhereUserIsNotCompetentFor(int employeeId,int id)
        {
            return Json(_teamLogic.GetUnitWhereUserIsNotCompetentFor(employeeId,id));
        }

        public IActionResult GetTasksWhereUserIsNotCompetentFor(int employeeId, int id)
        {
            return Json(_teamLogic.GetTasksWhereUserIsNotCompetentFor(employeeId,id));
        }


        [HttpPost]
        public IActionResult GetAllSections()
        {
            return Json(_preferenceLogic.GetAllSections());
        }
    }
}