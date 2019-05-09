using System;
using System.Collections.Generic;
using System.Text;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    public interface ITeamContext
    {
        int Getid(Team team);
        string Getnaam(Team team);
        IEnumerable<Team> GetAllTeams();

        IEnumerable<User> GetTeamUsers(User user);
    }
}
