﻿using System;
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
        private readonly PreferenceLogic _preferencesLogic = new PreferenceLogic();
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

        public IActionResult AddCompetences()
        {
            return View(_preferencesLogic.GetAllSections());
        }


    }
}