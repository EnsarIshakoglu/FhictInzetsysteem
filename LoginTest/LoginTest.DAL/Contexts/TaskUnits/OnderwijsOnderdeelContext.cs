using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts.TaskUnits
{

    class OnderwijsOnderdeelContext : IOnderwijsOnderdeelContext
    {
        private readonly string _connectionString =
            "Server=mssql.fhict.local;Database=dbi389621;User Id=dbi389621;Password=Ensar123;";


        public List<OnderwijsOnderdeel> GetAllOnderdelen()
        {
            List<OnderwijsOnderdeel> result = new List<OnderwijsOnderdeel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand($"SELECT Onderwijseenheid.Naam, OnderwijsTraject.Naam as Onderwijstraject " +
                                                    $"FROM Onderwijseenheid " +
                                                    $"JOIN Onderwijstraject ON Onderwijstraject.ID = onderwijseenheid.OnderwijstrajectID", conn);                              //TODO vervang met store procedure
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new OnderwijsOnderdeel
                    {
                        /*Id = (int)reader["Id"],
                        Naam = (string)reader["Naam"],
                        BegroteUren = (int)reader["BegroteUren"],
                        Factor = (int) reader["Factor"]*/
                    });
                }
                reader.Close();
                conn.Close();
            }
            return result;
        }

        public List<OnderwijsOnderdeel> GetAllOnderdelenByEenheid(OnderwijsEenheid eenheid)
        {
            List<OnderwijsOnderdeel> result = new List<OnderwijsOnderdeel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"SELECT Onderwijseenheid.Naam, OnderwijsTraject.Naam as Onderwijstraject " +
                                                    $"FROM Onderwijseenheid " +
                                                    //$"WHERE OnderwijseenheidID = {eenheid.Id}" +
                                                    $"JOIN Onderwijstraject ON Onderwijstraject.ID = onderwijseenheid.OnderwijstrajectID", conn);                              //vervang met store procedure
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new OnderwijsOnderdeel
                    {
                        /*Id = (int)reader["Id"],
                        Naam = (string)reader["Naam"],
                        BegroteUren = (int)reader["BegroteUren"],
                        Factor = (int)reader["Factor"]*/
                    });
                }
                reader.Close();
                conn.Close();
            }
            return result;
        }
    }
}
