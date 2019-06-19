using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DAL.Contexts;
using Models;
using Models.Enums;

namespace FHICTDeploymentSystem.DAL.Contexts
{
    public class TeamContext : ITeamContext
    {

        private readonly string _connectionString =
            "Server=mssql.fhict.local;Database=dbi389621;User Id=dbi389621;Password=Ensar123;";

        // vraag alle teams op(nummer en naam)
        public IEnumerable<Team> GetAllTeams()
        {
            var teams = new List<Team>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand("select ID, Naam from Team", connection);
                var reader = sqlCommand.ExecuteReader();


                while (reader.Read())
                {
                    teams.Add(new Team
                    {
                        Name = (string)reader["Naam"],
                        Id = (int)reader["ID"]
                    });
                }

                connection.Close();
            }

            return teams;
        }

        //vraag alle users op die bij een specifiek(ID) team horen(id en naam)
        public IEnumerable<User> GetTeamUsers(User user)
        {
            var userList = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand("GetAllEmployeesFromTeam", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@TeamId", user.TeamId));
                sqlCommand.Parameters.Add(new SqlParameter("@UserId", user.Id));

                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    //voeg namen van mensen toe die team id hebben
                    userList.Add(new User
                    {
                        Name = (string)reader["Name"],
                        Id = (int)reader["Id"]
                    });
                }

                connection.Close();
            }

            return userList;
        }

        public void RemoveUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand("RemoveEmployeeFromTeam", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@Id", user.Id));

                sqlCommand.ExecuteNonQuery();


                connection.Close();
            }
        }

        public void AddTeacher(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand("AddEmployeeToTeam", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@UserId", user.Id));
                sqlCommand.Parameters.Add(new SqlParameter("@TeamId", user.TeamId));

                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        public IEnumerable<User> GetAllUserWhithoutTeam(User user)
        {
            var userlist = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand("GetAllEmployeeNoTeam", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    //voeg namen van mensen toe die team id NULL hebben
                    userlist.Add(new User
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Username"],
                    });
                }

                connection.Close();
            }

            return userlist;

        }


        List<EducationObject> userCompetenceList = new List<EducationObject>();
        public IEnumerable<EducationObject> GetTeamMemberCompetences(User user)
        {     
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand("GetTeamMemberCompetences", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@EmployeeId", user.Id));

                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    userCompetenceList.Add(new EducationObject
                    {
                        Id = (int)reader["TaskId"],
                        Name = (string)reader["Code"],
                        Period = (int)reader["Period"],
                        Description = (string)reader["Description"]
                    });
                }

                connection.Close();
            }
            return userCompetenceList;
        }


        public EducationObject GetTeamMemberHours(int ID)
        {
            var hours = new EducationObject();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand("GetHours", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@EmployeeId", ID));

                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    hours.EstimatedHours = (int)reader["HoursPeriod1"];
                    hours.EstimatedHours2 = (int)reader["HoursPeriod2"];
                }

                connection.Close();
            }
            return hours;
        }

        public void SaveHours(User user, EducationObject hours)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand("SetHours", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@EmployeeId", user.Id));
                sqlCommand.Parameters.Add(new SqlParameter("@HoursPeriod1", hours.EstimatedHours));
                sqlCommand.Parameters.Add(new SqlParameter("@HoursPeriod2", hours.EstimatedHours2));

                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        public int Getid(Team team)
        {
            return team.Id;
        }

        public string Getnaam(Team team)
        {
            return team.Name;
        }


        public void AddSectionCompetence(int id, int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand("AddSectionCompetence", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@EmployeeId", employeeId));
                sqlCommand.Parameters.Add(new SqlParameter("@SectionId", id));

                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void AddUnitCompetence(int id, int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand("AddUnitCompetence", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@EmployeeId", employeeId));
                sqlCommand.Parameters.Add(new SqlParameter("@UnitId", id));

                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void AddUnitExecCompetence(int id, int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand("AddUnitExecCompetence", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@EmployeeId", employeeId));
                sqlCommand.Parameters.Add(new SqlParameter("@UnitExecId", id));


                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void AddTasksCompetence(int id, int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand("AddTaskCompetence", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@EmployeeId", employeeId));
                sqlCommand.Parameters.Add(new SqlParameter("@TaskId", id));
                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void RemoveCompetence(int id, int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand("DeleteCompetence", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add(new SqlParameter("@EmployeeId", employeeId));
                sqlCommand.Parameters.Add(new SqlParameter("@SectionId", id));


                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        //todo from here

        public List<EducationObject> GetSectionsWhereUserIsNotCompetentFor(int employeeId)
        {
            var returnValue = new List<EducationObject>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("GetSectionsWhereUserIsNotCompetentFor", connection) { CommandType = CommandType.StoredProcedure, };
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", employeeId));
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new EducationObject
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"]?.ToString(),
                        EducationType = EducationType.Task
                    });
                }
                reader.Close();
                connection.Close();
            }
            return returnValue;
        }

        public List<EducationObject> GetUnitTermExecsWhereUserIsNotCompetentFor(int employeeId, int id)
        {
            var unitTermExecutions = new List<EducationObject>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("GetUnitTermExecsWhereUserIsNotCompetentFor", connection) { CommandType = CommandType.StoredProcedure, };
                cmd.Parameters.Add(new SqlParameter("@UnitId", id));
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", employeeId));
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    unitTermExecutions.Add(new EducationObject
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"]?.ToString(),
                        EducationType = EducationType.UnitExec
                    });
                }
                connection.Close();
            }

            return unitTermExecutions;
        }

        public List<EducationObject> GetUnitWhereUserIsNotCompetentFor(int employeeId, int id)
        {
            var returnValue = new List<EducationObject>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("GetUnitsWhereUserIsNotCompetentFor", connection) { CommandType = CommandType.StoredProcedure, };
                cmd.Parameters.Add(new SqlParameter("@SectionId", id));
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", employeeId));
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new EducationObject
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"]?.ToString(),
                        EducationType = EducationType.Unit
                    });
                }
                reader.Close();
                connection.Close();
            }
            return returnValue;
        }

        public List<EducationObject> GetTasksWhereUserIsNotCompetentFor(int employeeId, int id)
        {
            var returnValue = new List<EducationObject>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("GetTasksWhereUserIsNotCompetentFor", connection) { CommandType = CommandType.StoredProcedure, };
                cmd.Parameters.Add(new SqlParameter("@UnitTermExecId", id));
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", employeeId));
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    returnValue.Add(new EducationObject
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"]?.ToString(),
                        EducationType = EducationType.Task
                    });
                }
                reader.Close();
                connection.Close();
            }
            return returnValue;
        }
    }
}
