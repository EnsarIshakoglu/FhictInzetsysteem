using System;
using System.Collections.Generic;
using System.Linq;
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

            var userList = new List<User>();
            var users = _teamLogic.GetTeamUsers(_user);

            foreach (var user in users)
            {
                userList.Add(user);
            }
            return View(userList);
        }

        public IActionResult RemoveUser(User _user)

        {
            _teamLogic.RemoveUser(_user);
            return RedirectToAction("ManageTeam");
        }

        public IActionResult GetTeamId(User user)
        {

        }

    }
}