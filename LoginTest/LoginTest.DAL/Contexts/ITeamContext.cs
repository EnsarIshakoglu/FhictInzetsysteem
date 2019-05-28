﻿using System;
using System.Collections.Generic;
using System.Text;
using FHICTDeploymentSystem.Models;

namespace DAL.Contexts
{
    public interface ITeamContext
    {
        int Getid(Team team);
        string Getnaam(Team team);

        IEnumerable<User> GetTeamUsers(User user);

        void RemoveUser(User _user);

        void EditUserInTeam(User user);
    }
}
