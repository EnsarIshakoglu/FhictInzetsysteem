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
                        Naam = reader["Naam"]?.ToString()
                    });
                }

                connection.Close();
            }

            return onderdelen;
        }

        public IEnumerable<OnderwijsTaak> GetAllOnderwijsTaken(int OnderdeelId)
        {
            var taken = new List<OnderwijsTaak>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAlleTaken", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OnderdeelID", OnderdeelId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    taken.Add(new OnderwijsTaak
                    {
                        Id = (int)reader["Id"],
                        Naam = reader["Naam"]?.ToString()
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

        public IEnumerable<OnderwijsOnderdeel> GetOnderdeelFromTraject(OnderwijsTraject onderwijsTraject)
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

        public IEnumerable<OnderwijsTaak> GetTakenFromEenheid(OnderwijsEenheid onderwijsEenheid)
        {
            var tasks = new List<OnderwijsTaak>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetTakenBinnenEenheid", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@EenheidID", onderwijsEenheid.Id));

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

        public Preference CheckTaakPreference(OnderwijsTaak taak, int userId) //todo kijken of het generic kan gemaakt worden
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

        public void AddOnderdeelPreference(OnderwijsOnderdeel onderdeel, int voorkeurWaarde, int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("AddOnderdeelVoorkeur", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OnderdeelID", onderdeel.Id));
                cmd.Parameters.Add(new SqlParameter("@VoorkeurWaarde", voorkeurWaarde));
                cmd.Parameters.Add(new SqlParameter("@GebruikersID", userId));

                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void UpdateOnderdeelPreference(OnderwijsOnderdeel onderdeel, int voorkeurWaarde, int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("UpdateOnderdeelVoorkeur", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@OnderdeelID", onderdeel.Id));
                cmd.Parameters.Add(new SqlParameter("@VoorkeurWaarde", voorkeurWaarde));
                cmd.Parameters.Add(new SqlParameter("@GebruikersID", userId));

                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
