using System;
using System.Collections.Generic;
using System.Text;
using DAL.Contexts;
using FHICTDeploymentSystem.DAL.Contexts;
using FHICTDeploymentSystem.Models;

namespace DAL
{
    public class TeamRepository
    {
        private readonly ITeamContext _context;

        public TeamRepository()
        {
            _context = new TeamContext();
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _context.GetAllTeams();
        }

        public IEnumerable<User> GetTeamUsers(User user)
        {
            return _context.GetTeamUsers(user);
        }

        public void RemoveUser(User _user)
        {
            _context.RemoveUser(_user);
        }

        public void AddTeacher(User _user)
        {
            _context.AddTeacher(_user);
        }

        public IEnumerable<User> GetAllUserNoTeam(User _user)
        {
            return _context.GetAllUserWhithoutTeam(_user);
        }

        public IEnumerable<EducationObject> GetTeamMemberCompetences(User user)
        {
            return _context.GetTeamMemberCompetences(user);
        }

        public void CreateVacancy(User user)
        {
            _context.CreateVacancy(user);
        }
    }
}
