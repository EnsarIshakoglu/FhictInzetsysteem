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
                List<OnderwijsTaak> tasks = new List<OnderwijsTaak>();

                tasks.AddRange(GetTakenFromTraject(new OnderwijsTraject
                {
                    Id = trajectPreference.Taak.Id,
                    Naam = trajectPreference.Taak.Naam
                }));

                foreach (var task in tasks)
                {
                    var taskPreference = CheckTaskPreference(task, userId);
                    if (taskPreference.Waarde == -1)
                    {
                        AddTaskPreference(task, trajectPreference.Waarde, userId);
                    }
                    else
                    {
                        UpdateTaskPreference(task, trajectPreference.Waarde, userId);
                    }
                }
            }

        }


        public IEnumerable<OnderwijsTraject> GetAllOnderwijsTrajecten()
        {
            var trajecten = new List<OnderwijsTraject>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAlleTrajecten", connection);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    trajecten.Add(new OnderwijsTraject
                    {
                        Id = (int) reader["Id"],
                        Naam = reader["Naam"]?.ToString()
                    });
                }

                connection.Close();
            }

            return trajecten;
        }

        public IEnumerable<OnderwijsEenheid> GetAllOnderwijsEenheden(int trajectId)
        {
            var eenheden = new List<OnderwijsEenheid>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAlleEenheden", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TrajectID", trajectId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    eenheden.Add(new OnderwijsEenheid
                    {
                        Id = (int)reader["Id"],
                        Naam = reader["Naam"]?.ToString()
                    });
                }

                connection.Close();
            }

            return eenheden;
        }

        public IEnumerable<OnderwijsTaak> GetAllTasks(int EdUnitId)
        {
            var taken = new List<OnderwijsTaak>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllTasks", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@EdUnitId", EdUnitId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    taken.Add(new OnderwijsTaak
                    {
                        Id = (int)reader["Id"],
                        Naam = reader["Code"]?.ToString()
                    });
                }

                connection.Close();
            }

            return taken;
        }

        public IEnumerable<OnderwijsTaak> GetTakenFromTraject(OnderwijsTraject onderwijsTraject)
        {
            var tasks = new List<OnderwijsTaak>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetTakenBinnenTraject", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TrajectID", onderwijsTraject.Id));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new OnderwijsTaak
                    {
                        Id = (int)reader["Id"],
                    });
                }

                connection.Close();
            }

            return tasks;
        }

        public Preference CheckTaskPreference(OnderwijsTaak task, int userId) //todo kijken of het generic kan gemaakt worden
        {
            var taskPreference = new Preference
            {
                Taak = task,
                Waarde = -1
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
                        taskPreference.Waarde = (int)reader["Priority"];
                    }

                connection.Close();
                }

            return taskPreference;
        }

        public void AddTaskPreference(OnderwijsTaak task, int priority, int userId)
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

        public void UpdateTaskPreference(OnderwijsTaak task, int priority, int userId)
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
