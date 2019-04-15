using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL.Contexts
{
    public class OnderwijsTrajectContext : IOnderwijsTrajectContext
    {
        private readonly string _connectionString =
            "Server=mssql.fhict.local;Database=dbi389621;User Id=dbi389621;Password=Ensar123;";

        public List<OnderwijsTraject> GetAllTrajects()
        {
            List<OnderwijsTraject> result = new List<OnderwijsTraject>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand($"SELECT * FROM dbo.OnderwijsTraject ", conn);                              //vervang met store procedure
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new OnderwijsTraject
                    {
                       Id = (int) reader["Id"],
                       Naam = (string) reader["Naam"],
                    });
                }
                reader.Close();
                conn.Close();
            }
            return result;
        }
    }
}
