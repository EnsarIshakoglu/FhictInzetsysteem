using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FHICTDeploymentSystem.Logic;
using FHICTDeploymentSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace FHICTDeploymentSystem.Controllers
{
    public class TeamController : Controller
    {
        private readonly TeamLogic _teamLogic = new TeamLogic(); 

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
            _teamLogic.GetTeamMemberCompetences(user);
            return View(user);
        }

        public IActionResult CreateVacancy(User user)
        {
            var sid = User.Claims.First(c => c.Type.Equals(ClaimTypes.Sid)).Value;
            int.TryParse(sid, out int teamId);
            user.TeamId = teamId;
            _teamLogic.CreateVacancy(user);
            return RedirectToAction("ManageTeam");
        }

    }
}