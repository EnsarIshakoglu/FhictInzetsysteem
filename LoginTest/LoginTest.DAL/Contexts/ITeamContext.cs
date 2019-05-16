using System;
using System.Collections.Generic;
using System.Text;
using FHICTDeploymentSystem.Models;

namespace FHICTDeploymentSystem.DAL.Contexts
{
    public interface ITeamContext
    {
        int Getid(Team team);
        string Getnaam(Team team);
        IEnumerable<Team> GetAllTeams();

        IEnumerable<User> GetTeamUsers(User user);

        void RemoveUser(User _user);
    }
}
