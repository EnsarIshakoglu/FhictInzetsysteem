using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts.TaskUnits
{
    class OnderwijsTaakContext : IOnderwijsTaakContext
    {
        private readonly string _connectionString =
            "Server=mssql.fhict.local;Database=dbi389621;User Id=dbi389621;Password=Ensar123;";

        public List<OnderwijsTaak> GetAllTaken()
        {
            List<OnderwijsTaak> result = new List<OnderwijsTaak>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand($"Select * FROM Onderwijstaak", conn);                              //vervang met store procedure
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new OnderwijsTaak
                    {
                        Id = (int)reader["Id"],
                        Naam = (string)reader["Naam"],
                        BegroteUren = (int)reader["BegroteUren"],
                        Factor = (int)reader["Factor"],
                        Periode = (string)reader["Periode"]
                    });
                }
                reader.Close();
                conn.Close();
            }
            return result;
        }

        public List<OnderwijsTaak> GetAllTakenByOnderdeel(OnderwijsOnderdeel onderdeel)
        {
            List<OnderwijsTaak> result = new List<OnderwijsTaak>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand($"Select * FROM Onderwijstaak WHERE OnderwijsonderdeelID = {onderdeel.Id}", conn);                              //vervang met store procedure
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new OnderwijsTaak
                    {
                        Id = (int)reader["Id"],
                        Naam = (string)reader["Naam"],
                        BegroteUren = (int)reader["BegroteUren"],
                        Factor = (int)reader["Factor"],
                        Periode = (string)reader["Periode"]
                    });
                }
                reader.Close();
                conn.Close();
            }
            return result;
        }
    }
}
