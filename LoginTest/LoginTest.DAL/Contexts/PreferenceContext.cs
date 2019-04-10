using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
                    trajecten.Add(new OnderwijsTraject(reader["Naam"]?.ToString(), (int)reader["Id"]));
                }

                connection.Close();
            }

            return trajecten;
        }

        public IEnumerable<OnderwijsTaak> GetTasksFromTraject(OnderwijsTraject onderwijsTraject)
        {
            var taken = new List<OnderwijsTaak>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand($"SELECT a.Id, a.Naam FROM Onderwijstaak a \r\nINNER JOIN onderwijsonderdeel b ON a.OnderwijsonderdeelID = b.ID \r\nINNER JOIN Onderwijseenheid c ON b.OnderwijseenheidID = c.ID \r\nWHERE c.OnderwijstrajectID = '{onderwijsTraject.Id}'", connection);
                var reader = sqlCommand.ExecuteReader();

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

        public IEnumerable<Voorkeur> GetPreferencesFromTraject(OnderwijsTraject traject, int IdUser)
        {
            var preferences = new List<Voorkeur>();
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
                        preferences.Add(new Voorkeur(task, (int)reader["Voorkeur waarde"]));
                    }

                    connection.Close();
                }
            }

            return preferences;
        }
    }
}
