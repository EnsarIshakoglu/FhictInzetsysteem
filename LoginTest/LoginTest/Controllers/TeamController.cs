using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inzetsysteem.Logic;
using Inzetsysteem.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inzetsysteem.Controllers
{
    public class TeamController : Controller
    {
        private readonly TeamLogic _teamLogic = new TeamLogic(); 

        public IActionResult TeamBeheren()
        {
            User _user = new User();
            _user.TeamId = 1;

            var userlist = new List<User>();
            var users = _teamLogic.GetTeamUsers(_user);

            foreach (var user in users)
            {
                userlist.Add(user);
            }
            return View(userlist);
        }

        public IActionResult RemoveUser(User _user)
        {
            _teamLogic.RemoveUser(_user);
            return RedirectToAction("TeamBeheren");
        }


    }
}