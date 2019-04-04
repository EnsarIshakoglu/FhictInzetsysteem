using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace LoginTest.DAL
{
    public class UserContext : IUserContext
    {
        private readonly string connectionString =
            "Server=mssql.fhict.local;Database=dbi389621;User Id=dbi389621;Password=Ensar123;";

        public bool Login(string userName, string password)
        {
            bool loginSuccesfull = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand($"SELECT TOP 1 Inlognaam,Wachtwoord from dbo.Gebruiker WHERE Inlognaam = '{userName}' and Wachtwoord = '{password}'", conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    loginSuccesfull = true;
                }
                reader.Close();
                conn.Close();
            }
            return loginSuccesfull;
        }
    }
}
