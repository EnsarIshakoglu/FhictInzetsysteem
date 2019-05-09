using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    public class PreferenceContext : IPreferenceContext
    {
        private readonly string _connectionString =
            "Server=mssql.fhict.local;Database=dbi389621;User Id=dbi389621;Password=Ensar123;";

        public void SaveEdSectionPreferences(IEnumerable<Preference> sectionPreferences, int userId)
        {
            foreach (var sectionPreference in sectionPreferences)
            {
                List<Task> tasks = new List<Task>();

                tasks.AddRange(GetTasksFromSection(new Section
                {
                    Id = sectionPreference.Task.Id,
                    Name = sectionPreference.Task.Name
                }));

                foreach (var task in tasks)
                {
                    var taskPreference = CheckTaskPreference(task, userId);
                    if (taskPreference.Value == -1)
                    {
                        AddTaskPreference(task, sectionPreference.Value, userId);
                    }
                    else
                    {
                        UpdateTaskPreference(task, sectionPreference.Value, userId);
                    }
                }
            }

        }


        public IEnumerable<Section> GetAllSections()
        {
            var sections = new List<Section>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllSections", connection);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    sections.Add(new Section
                    {
                        Id = (int) reader["Id"],
                        Name = reader["Name"]?.ToString()
                    });
                }

                connection.Close();
            }

            return sections;
        }

        public IEnumerable<Unit> GetAllUnits(int SectionId)
        {
            var units = new List<Unit>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetUnits", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@EdSectionId", SectionId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    units.Add(new Unit
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"]?.ToString()
                    });
                }

                connection.Close();
            }

            return units;
        }

        public IEnumerable<Task> GetAllTasks(int UnitId)
        {
            var taken = new List<Task>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllTasks", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@EdUnitId", UnitId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    taken.Add(new Task
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Code"]?.ToString()
                    });
                }

                connection.Close();
            }

            return taken;
        }

        public IEnumerable<Task> GetTasksFromSection(Section Section)
        {
            var tasks = new List<Task>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetTasksWithinSection", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@SectionId", Section.Id));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new Task
                    {
                        Id = (int)reader["Id"],
                    });
                }

                connection.Close();
            }

            return tasks;
        }

        public Preference CheckTaskPreference(Task task, int userId) //todo kijken of het generic kan gemaakt worden
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

        public void AddTaskPreference(Task task, int priority, int userId)
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

        public void UpdateTaskPreference(Task task, int priority, int userId)
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
