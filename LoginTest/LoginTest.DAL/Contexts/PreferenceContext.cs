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
                List<OnderwijsTask> tasks = new List<OnderwijsTask>();

                tasks.AddRange(GetTakenFromTraject(new EducationSection
                {
                    Id = trajectPreference.Task.Id,
                    Name = trajectPreference.Task.Name
                }));

                foreach (var Task in tasks)
                {
                    var TaskPreference = CheckTaskPreference(Task, userId);
                    if (TaskPreference.Value == -1)
                    {
                        AddTaskPreference(Task, trajectPreference.Value, userId);
                    }
                    else
                    {
                        UpdateTaskPreference(Task, trajectPreference.Value, userId);
                    }
                }
            }

        }


        public IEnumerable<EducationSection> GetAllOnderwijsTrajecten()
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

        public IEnumerable<OnderwijsOnderdeel> GetAllOnderwijsOnderdelen(int EenheidId)
        {
            var onderdelen = new List<OnderwijsOnderdeel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAlleOnderdelen", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@EenheidID", EenheidId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    onderdelen.Add(new OnderwijsOnderdeel
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"]?.ToString()
                    });
                }

                connection.Close();
            }

            return onderdelen;
        }

        public IEnumerable<OnderwijsTask> GetAllOnderwijsTaken(int OnderdeelId)
        {
            var taken = new List<OnderwijsTask>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAlleTaken", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OnderdeelID", OnderdeelId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    taken.Add(new OnderwijsTask
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"]?.ToString()
                    });
                }

                connection.Close();
            }

            return taken;
        }

        public IEnumerable<OnderwijsTask> GetTakenFromTraject(EducationSection onderwijsTraject)
        {
            var tasks = new List<OnderwijsTask>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetTakenBinnenTraject", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TrajectID", onderwijsTraject.Id));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new OnderwijsTask
                    {
                        Id = (int)reader["Id"],
                    });
                }

                connection.Close();
            }

            return tasks;
        }

        public IEnumerable<OnderwijsOnderdeel> GetOnderdeelFromTraject(EducationSection onderwijsTraject)
        {
            var tasks = new List<OnderwijsOnderdeel>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetOnderdelenBinnenTraject", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TrajectID", onderwijsTraject.Id));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new OnderwijsOnderdeel
                    {
                        Id = (int)reader["Id"],
                    });
                }

                connection.Close();
            }

            return tasks;
        }

        public IEnumerable<OnderwijsTask> GetTakenFromEenheid(EducationUnit onderwijsEenheid)
        {
            var tasks = new List<OnderwijsTask>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetTakenBinnenEenheid", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@EenheidID", onderwijsEenheid.Id));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new OnderwijsTask
                    {
                        Id = (int)reader["Id"],
                    });
                }

                connection.Close();
            }

            return tasks;
        }

        public Preference CheckTaskPreference(OnderwijsTask Task, int userId) //todo kijken of het generic kan gemaakt worden
        {
            var TaskPreference = new Preference
            {
                Task = Task,
                Value = -1
            };

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("CheckTaskPreference", connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@TaskID", Task.Id));
                    cmd.Parameters.Add(new SqlParameter("@GebruikersID", userId));

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        TaskPreference.Value = (int)reader["Preference Value"];
                    }

                connection.Close();
                }

            return TaskPreference;
        }

        public void AddTaskPreference(OnderwijsTask Task, int PreferenceValue, int userId)
        {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("AddTaskPreference", connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@TaskID", Task.Id));
                    cmd.Parameters.Add(new SqlParameter("@PreferenceValue", PreferenceValue));
                    cmd.Parameters.Add(new SqlParameter("@GebruikersID", userId));

                    cmd.ExecuteNonQuery();

                    connection.Close();
                }
        }

        public void UpdateTaskPreference(OnderwijsTask Task, int PreferenceValue, int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("UpdateTaskPreference", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TaskID", Task.Id));
                cmd.Parameters.Add(new SqlParameter("@PreferenceValue", PreferenceValue));
                cmd.Parameters.Add(new SqlParameter("@GebruikersID", userId));

                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public Preference CheckOnderdeelPreference(OnderwijsOnderdeel onderdeel, int userId)
        {
            var onderdeelPreference = new Preference
            {
                Task = onderdeel
            };

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("CheckOnderdeelPreference", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OnderdeelID", onderdeel.Id));
                cmd.Parameters.Add(new SqlParameter("@GebruikersID", userId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    onderdeelPreference.Value = (int)reader["Preference Value"];
                }

                connection.Close();
            }

            return onderdeelPreference;
        }

        public void AddOnderdeelPreference(OnderwijsOnderdeel onderdeel, int PreferenceValue, int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("AddOnderdeelPreference", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OnderdeelID", onderdeel.Id));
                cmd.Parameters.Add(new SqlParameter("@PreferenceValue", PreferenceValue));
                cmd.Parameters.Add(new SqlParameter("@GebruikersID", userId));

                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void UpdateOnderdeelPreference(OnderwijsOnderdeel onderdeel, int PreferenceValue, int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("UpdateOnderdeelPreference", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OnderdeelID", onderdeel.Id));
                cmd.Parameters.Add(new SqlParameter("@PreferenceValue", PreferenceValue));
                cmd.Parameters.Add(new SqlParameter("@GebruikersID", userId));

                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
