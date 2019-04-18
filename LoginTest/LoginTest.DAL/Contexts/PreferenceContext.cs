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

        public IEnumerable<OnderwijsTraject> GetAllOnderwijsTrajecten()
        {
            var trajecten = new List<OnderwijsTraject>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand($"select Id, Naam from Onderwijstraject", connection);
                var reader = sqlCommand.ExecuteReader();

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

        public IEnumerable<OnderwijsTaak> GetTasksFromTraject(OnderwijsTraject onderwijsTraject)
        {
            var tasks = new List<OnderwijsTaak>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand($"SELECT a.Id, a.Naam FROM Onderwijstaak a \r\nINNER JOIN onderwijsonderdeel b ON a.OnderwijsonderdeelID = b.ID \r\nINNER JOIN Onderwijseenheid c ON b.OnderwijseenheidID = c.ID \r\nWHERE c.OnderwijstrajectID = '{onderwijsTraject.Id}'", connection);
                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new OnderwijsTaak
                    {
                        Id = (int)reader["Id"],
                        Naam = reader["Naam"]?.ToString()
                    });
                }

                connection.Close();
            }

            return tasks;
        }

        public IEnumerable<Preference> GetPreferencesFromTraject(OnderwijsTraject traject, int IdUser)
        {
            var preferences = new List<Preference>();
            var tasks = GetTasksFromTraject(traject);

            foreach (var task in tasks)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var sqlCommand = new SqlCommand($"SELECT G.Naam, [Voorkeur waarde] FROM Voorkeuren\r\ninner join Gebruiker G on Voorkeuren.GebruikerID = G.ID\r\nWHERE OnderwijstaakID = {task.Id} \r\nand G.ID = {IdUser}", connection);
                    var reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        preferences.Add(new Preference
                        {
                            Taak = task,
                            Waarde = (int) reader["Voorkeur waarde"]
                        });
                    }

                    connection.Close();
                }
            }

            return preferences;
        }

        public void SaveTrajectPreferences(IEnumerable<Preference> trajectPreferences, int userId)
        {
            foreach (var trajectPreference in trajectPreferences)
            {
                List<OnderwijsTaak> tasks = new List<OnderwijsTaak>();

                tasks.AddRange(GetTasksFromTraject(new OnderwijsTraject
                {
                    Id = trajectPreference.Taak.Id,
                    Naam = trajectPreference.Taak.Naam
                }));

                foreach (var taak in tasks)
                {
                    var taakPreference = CheckTaakPreference(taak, userId);
                    if (taakPreference.Waarde == -1)
                    {
                        AddTaakPreference(taak, trajectPreference.Waarde, userId);
                    }
                    else
                    {
                        UpdateTaakPreference(taak, trajectPreference.Waarde, userId);
                    }
                }
            }

        }

        public Preference CheckTaakPreference(OnderwijsTaak taak, int userId)
        {
            var taakPreference = new Preference
            {
                Taak = taak,
                Waarde = -1
            };

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("CheckTaakVoorkeur", connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@TaakID", taak.Id));
                    cmd.Parameters.Add(new SqlParameter("@GebruikersID", userId));

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        taakPreference.Waarde = (int)reader["Voorkeur waarde"];
                    }

                connection.Close();
                }

            return taakPreference;
        }

        public void AddTaakPreference(OnderwijsTaak taak, int voorkeurWaarde, int userId)
        {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("AddTaakVoorkeur", connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@TaakID", taak.Id));
                    cmd.Parameters.Add(new SqlParameter("@VoorkeurWaarde", voorkeurWaarde));
                    cmd.Parameters.Add(new SqlParameter("@GebruikersID", userId));

                    cmd.ExecuteNonQuery();

                    connection.Close();
                }
        }

        public void UpdateTaakPreference(OnderwijsTaak taak, int voorkeurWaarde, int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("UpdateTaakVoorkeur", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TaakID", taak.Id));
                cmd.Parameters.Add(new SqlParameter("@VoorkeurWaarde", voorkeurWaarde));
                cmd.Parameters.Add(new SqlParameter("@GebruikersID", userId));

                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public Preference CheckOnderdeelPreference(OnderwijsOnderdeel onderdeel, int userId)
        {
            var onderdeelPreference = new Preference
            {
                Taak = onderdeel
            };

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("CheckOnderdeelVoorkeur", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OnderdeelID", onderdeel.Id));
                cmd.Parameters.Add(new SqlParameter("@GebruikersID", userId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    onderdeelPreference.Waarde = (int)reader["Voorkeur waarde"];
                }

                connection.Close();
            }

            return onderdeelPreference;
        }
    }
}
