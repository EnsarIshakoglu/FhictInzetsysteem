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

        public void SaveTrajectPreferences(IEnumerable<Preference> trajectPreferences, int userId)
        {
            foreach (var trajectPreference in trajectPreferences)
            {
                List<Task> tasks = new List<Task>();

                tasks.AddRange(GetTakenFromTraject(new EducationSection
                {
                    Id = trajectPreference.Task.Id,
                    Name = trajectPreference.Task.Name
                }));

                foreach (var task in tasks)
                {
                    var taskPreference = CheckTaskPreference(task, userId);
                    if (taskPreference.Value == -1)
                    {
                        AddTaskPreference(task, trajectPreference.Value, userId);
                    }
                    else
                    {
                        UpdateTaskPreference(task, trajectPreference.Value, userId);
                    }
                }
            }

        }


        public IEnumerable<EducationSection> GetAllEducationSectionen()
        {
            var trajecten = new List<EducationSection>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAlleTrajecten", connection);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    trajecten.Add(new EducationSection
                    {
                        Id = (int) reader["Id"],
                        Name = reader["Name"]?.ToString()
                    });
                }

                connection.Close();
            }

            return trajecten;
        }

        public IEnumerable<EducationUnit> GetAllOnderwijsEenheden(int trajectId)
        {
            var eenheden = new List<EducationUnit>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAlleEenheden", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TrajectID", trajectId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    eenheden.Add(new EducationUnit
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"]?.ToString()
                    });
                }

                connection.Close();
            }

            return eenheden;
        }

        public IEnumerable<Task> GetAllTasks(int EdUnitId)
        {
            var taken = new List<Task>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllTasks", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@EdUnitId", EdUnitId));

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

        public IEnumerable<Task> GetTakenFromTraject(EducationSection EducationSection)
        {
            var tasks = new List<Task>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetTakenBinnenTraject", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TrajectID", EducationSection.Id));

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
