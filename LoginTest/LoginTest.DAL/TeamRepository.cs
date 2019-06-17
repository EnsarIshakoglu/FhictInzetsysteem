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

        public EducationObject GetTeamMemberHours(int ID)
        {
            return _context.GetTeamMemberHours(ID);
        }

        public void SaveHours(User user, EducationObject hours)
        {
            _context.SaveHours(user, hours);
        }

        public void RemoveCompetence(int id)
        {
            _context.RemoveCompetence(id);
        }

        public void AddSectionCompetence(int id)
        {
            _context.AddSectionCompetence(id);
        }


        public void AddUnitCompetence(int id)
        {
            _context.AddUnitCompetence(id);
        }


        public void AddUnitExecCompetence(int id)
        {
            _context.AddUnitExecCompetence(id);
        }


        public void AddTasksCompetence(int id)
        {
            _context.AddTasksCompetence(id);
        }
    }
}
