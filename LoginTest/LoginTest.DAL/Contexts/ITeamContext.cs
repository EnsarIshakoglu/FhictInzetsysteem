using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace DAL.Contexts
{
    public interface ITeamContext
    {
        int Getid(Team team);
        string Getnaam(Team team);
        IEnumerable<Team> GetAllTeams();

        IEnumerable<User> GetTeamUsers(User user);

        IEnumerable<User> GetAllUserWhithoutTeam(User user);

        void RemoveUser(User _user);
        void AddTeacher(User _user);

        IEnumerable<EducationObject> GetTeamMemberCompetences(User _user);
        EducationObject GetTeamMemberHours(int ID);
        void SaveHours(User user, EducationObject hours);
        void AddSectionCompetence(int id, int employeeId);
        void AddUnitCompetence(int id, int employeeId);
        void AddUnitExecCompetence(int id, int employeeId);
        void AddTasksCompetence(int id, int employeeId);
        void RemoveCompetence(int id, int employeeId);
        List<EducationObject> GetSectionsWhereUserIsNotCompetentFor(int employeeId);
        List<EducationObject> GetUnitTermExecsWhereUserIsNotCompetentFor(int employeeId, int id);
        List<EducationObject> GetUnitWhereUserIsNotCompetentFor(int employeeId, int id);
        List<EducationObject> GetTasksWhereUserIsNotCompetentFor(int employeeId, int id);
    }
}
