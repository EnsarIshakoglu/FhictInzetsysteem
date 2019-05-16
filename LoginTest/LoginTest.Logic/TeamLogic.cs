﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FHICTDeploymentSystem.DAL;
using FHICTDeploymentSystem.Models;
using Newtonsoft.Json;

namespace FHICTDeploymentSystem.Logic
{
    public class TeamLogic
    {
        private readonly TeamRepository _repo = new TeamRepository();

        public IEnumerable<User> GetTeamUsers(User user)
        {
            return _repo.GetTeamUsers(user);
        }

        public IEnumerable<Team> GetTeam(int id)
        {
            return GetTeam(id); //doe een keer iets goed leon
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _repo.GetAllTeams();
        }

        public void RemoveUser(User _user)
        {
            _repo.RemoveUser(_user);
        }

    }
}