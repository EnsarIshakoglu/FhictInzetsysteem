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
                SqlCommand command = new SqlCommand("SELECT Onderwijstaak.Naam, Onderwijstaak.Periode as Periode, Onderwijstaak.[Aantal begrote uren], Onderwijstaak.Factor, Onderwijsonderdeel.Naam as Onderwijsonderdeel " +
                                                    "FROM Onderwijstaak " +
                                                    "JOIN Onderwijsonderdeel " +
                                                    "ON dbo.Onderwijsonderdeel.ID = OnderwijseenheidID", conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new OnderwijsTaak
                    {
                        /*Id = (int)reader["Id"],
                        Naam = (string)reader["Naam"],
                        BegroteUren = (int)reader["BegroteUren"],
                        Factor = (int)reader["Factor"],
                        Periode = (string)reader["Periode"]*/
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

                //SqlCommand command = new SqlCommand($"Select * FROM Onderwijstaak WHERE OnderwijsonderdeelID = {onderdeel.Id}", conn);                              //vervang met store procedure
                SqlCommand command = new SqlCommand("SELECT Onderwijstaak.Naam, Onderwijstaak.Periode as Periode, Onderwijstaak.[Aantal begrote uren], Onderwijstaak.Factor, Onderwijsonderdeel.Naam as Onderwijsonderdeel " +
                                         "FROM Onderwijstaak " +
                                         //$"WHERE Onderwijsonderdeel.Naam = {onderdeel.Naam}" +
                                         "JOIN Onderwijsonderdeel " +
                                         "ON dbo.Onderwijsonderdeel.ID = OnderwijseenheidID",conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new OnderwijsTaak
                    {
                        /*Id = (int)reader["Id"],
                        Naam = (string)reader["Naam"],
                        BegroteUren = (int)reader["BegroteUren"],
                        Factor = (int)reader["Factor"],
                        Periode = (string)reader["Periode"],
                        Onderdeel = (string)reader[""]*/
                    });
                }
                reader.Close();
                conn.Close();
            }
            return result;
        }
    }
}
