using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using FHICTDeploymentSystem.Models;

namespace FHICTDeploymentSystem.DAL.Contexts
{
    public class PreferenceContext : IPreferenceContext
    {
        private readonly string _connectionString =
            "Server=mssql.fhict.local;Database=dbi389621;User Id=dbi389621;Password=Ensar123;";

        public IEnumerable<EducationObject> GetAllSections()
        {
            var sections = new List<EducationObject>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllSections", connection);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    sections.Add(new EducationObject
                    {
                        Id = (int) reader["Id"],
                        Name = reader["Name"]?.ToString(),
                        EducationType = EducationType.Section
                    });
                }

                connection.Close();
            }

            return sections;
        }

        public IEnumerable<EducationObject> GetAllUnits(int SectionId)
        {
            var units = new List<EducationObject>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllUnits", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@SectionId", SectionId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    units.Add(new EducationObject
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"]?.ToString(),
                        EducationType = EducationType.Unit
                    });
                }

                connection.Close();
            }

            return units;
        }

        public IEnumerable<EducationObject> GetAllTasks(int unitExecId)
        {
            var taken = new List<EducationObject>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllTasksFromUnitExecId", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@UnitExecId", unitExecId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    taken.Add(new EducationObject
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Code"]?.ToString(),
                        Description = reader["Description"]?.ToString(),
                        Explanation = reader["Explanation"]?.ToString(),
                        Period = (int)reader["Period"],
                        EstimatedHours = (int)reader["Hours"],
                        EducationType = EducationType.Task
                    });
                }

                connection.Close();
            }

            return taken;
        }

        public IEnumerable<EducationObject> GetTasksFromSection(EducationObject section)
        {
            var tasks = new List<EducationObject>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetTasksWithinSection", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@SectionId", section.Id));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new EducationObject
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Code"]?.ToString(),
                        EducationType = EducationType.Task
                    });
                }

                connection.Close();
            }

            return tasks;
        }

        public IEnumerable<EducationObject> GetTasksFromUnit(int unitId)
        {
            var tasks = new List<EducationObject>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetTasksWithinUnit", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@UnitId", unitId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new EducationObject
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Code"]?.ToString(),
                        EducationType = EducationType.Task
                    });
                }

                connection.Close();
            }

            return tasks;
        }

        public IEnumerable<EducationObject> GetTasksFromUnitExecution(int unitExecutionId)
        {
            throw new NotImplementedException();
        }

        public Preference CheckTaskPreference(EducationObject task, int userId) //todo kijken of het generic kan gemaakt worden
        {
            var taskPreference = new Preference
            {
                Task = task,
                Value = -1
            };

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("CheckTaskPreference", connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@TaskId", task.Id));
                    cmd.Parameters.Add(new SqlParameter("@EmployeeId", userId));

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        taskPreference.Value = (int)reader["Priority"];
                    }

                connection.Close();
                }

            return taskPreference;
        }

        public void AddTaskPreference(EducationObject task, int priority, int userId)
        {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("AddTaskPreference", connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@TaskId", task.Id));
                    cmd.Parameters.Add(new SqlParameter("@Priority", priority));
                    cmd.Parameters.Add(new SqlParameter("@EmployeeId", userId));

                    cmd.ExecuteNonQuery();

                    connection.Close();
                }
        }

        public void UpdateTaskPreference(EducationObject task, int priority, int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("UpdateTaskPreference", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TaskId", task.Id));
                cmd.Parameters.Add(new SqlParameter("@Priority", priority));
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", userId));

                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
