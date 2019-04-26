using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    public class OnderwijsEenheidContext : IOnderwijsEenheidContext
    {
        private readonly string _connectionString =
            "Server=mssql.fhict.local;Database=dbi389621;User Id=dbi389621;Password=Ensar123;";

        public List<OnderwijsEenheid> GetAllEenheden()
        {
            List<OnderwijsEenheid> result = new List<OnderwijsEenheid>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand($"SELECT Onderwijseenheid.Naam, OnderwijsTraject.Naam as Onderwijstraject " +
                                                    $"FROM Onderwijseenheid " +
                                                    $"JOIN Onderwijstraject " +
                                                    $"ON Onderwijstraject.ID = onderwijseenheid.OnderwijstrajectID", conn);                              //vervang met store procedure

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new OnderwijsEenheid
                    {
                        Naam = (string)reader["Naam"],
                        Traject = (string)reader["Onderwijstraject"]
                    });
                }
                reader.Close();
                conn.Close();
            }
            return result;
        }

        public List<OnderwijsEenheid> GetAllEenhedenByTraject(OnderwijsTraject traject)
        {
            List<OnderwijsEenheid> result = new List<OnderwijsEenheid>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"SELECT Onderwijseenheid.Naam, OnderwijsTraject.Naam as Onderwijstraject " +
                                                    $"FROM Onderwijseenheid " +
                                                    //$"WHERE OnderwijsTraject = '{traject.Naam}' " +
                                                    $"JOIN Onderwijstraject " +
                                                    $"ON Onderwijstraject.ID = onderwijseenheid.OnderwijstrajectID", conn);                              //vervang met store procedure
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new OnderwijsEenheid
                    {
                        Naam = (string)reader["Naam"],
                        Traject = (string)reader["Onderwijstraject"]
                    });
                }
                reader.Close();
                conn.Close();
            }
            return result;
        }
    }
}
