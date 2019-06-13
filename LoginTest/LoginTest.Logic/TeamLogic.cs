using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using FHICTDeploymentSystem.DAL;
using FHICTDeploymentSystem.Models;

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

        public void AddTeacher(User _user)
        {
            _repo.AddTeacher(_user);
        }

        public IEnumerable<User> GetAllUserWhitoutTeam(User _user)
        {
            return _repo.GetAllUserNoTeam(_user);
        }

        public IEnumerable<User> GetEmployeeCompetences(User _user)
        {
            return _repo.GetEmployeeCompetences(_user);

        }

        public void CreateVacancy(User user)
        {
           _repo.CreateVacancy(user);
        }

    }
}
