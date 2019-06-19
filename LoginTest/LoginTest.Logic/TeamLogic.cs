using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using FHICTDeploymentSystem.DAL;
using Models;

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

        public IEnumerable<EducationObject> GetTeamMemberCompetences(User _user)
        {
            return _repo.GetTeamMemberCompetences(_user);
        }

        public EducationObject GetTeamMemberHours(int ID)
        {
            return _repo.GetTeamMemberHours(ID);
        }

        public void SaveHours(User user, EducationObject hours)
        {
            _repo.SaveHours(user, hours);
        }

        public void RemoveCompetence(int id, int employeeId)
        {
            _repo.RemoveCompetence(id, employeeId);
        }

        public void AddSectionCompetence(int id, int employeeId)
        {
            _repo.AddSectionCompetence(id, employeeId);
        }


        public void AddUnitCompetence(int id, int employeeId)
        {
            _repo.AddUnitCompetence(id, employeeId);
        }


        public void AddUnitExecCompetence(int id, int employeeId)
        {
            _repo.AddUnitExecCompetence(id, employeeId);
        }


        public void AddTasksCompetence(int id, int employeeId)
        {
            _repo.AddTasksCompetence(id, employeeId);
        }

        public List<EducationObject> GetSectionsWhereUserIsNotCompetentFor(int employeeId)
        {
            return _repo.GetSectionsWhereUserIsNotCompetentFor(employeeId);
        }

        public List<EducationObject> GetTermExecsWhereUserIsNotCompetentFor(int employeeId, int id)
        {
            return _repo.GetTermExecsWhereUserIsNotCompetentFor(employeeId,id);
        }

        public List<EducationObject> GetUnitWhereUserIsNotCompetentFor(int employeeId,int id)
        {
            return _repo.GetUnitWhereUserIsNotCompetentFor(employeeId,id);
        }

        public List<EducationObject> GetTasksWhereUserIsNotCompetentFor(int employeeId, int id)
        {
            return _repo.GetTasksWhereUserIsNotCompetentFor(employeeId,id);
        }
    }
}
