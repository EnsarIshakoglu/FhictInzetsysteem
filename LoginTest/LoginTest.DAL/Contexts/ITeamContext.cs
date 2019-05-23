using System;
using System.Collections.Generic;
using System.Text;
using FHICTDeploymentSystem.Models;

namespace DAL.Contexts
{
    public interface ITeamContext
    {
        int Getid(Team team);
        string Getnaam(Team team);
        IEnumerable<Team> GetAllTeams();

        IEnumerable<User> GetTeamUsers(User user);

        IEnumerable<User> GetAllUserNoTeam(User user);

        void RemoveUser(User _user);
    }
}
