using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Inzetsysteem.Models;

namespace Inzetsysteem.DAL
{
    public class UserContext : IUserContext
    {
        private readonly string _connectionString =
            "Server=mssql.fhict.local;Database=dbi389621;User Id=dbi389621;Password=Ensar123;";

        public bool Login(User user)
        {
            bool loginSuccesfull = false;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand($"SELECT TOP 1 Id, Username,Password from dbo.User WHERE Username = '{user.Username}' and Password = '{user.Password}'", conn);
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

        public IEnumerable<string> GetUserRoles(User user)
        {
            var roles = new List<string>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand($"select G.Name, A.AutorisatieName \r\nfrom dbo.[Tussentabel gebruiker - Authorisatie] as TAG\r\ninner join Gebruiker G on TAG.GebruikerID = G.ID\r\ninner join Authorisatie A on TAG.AuthorisatieID = A.ID where G.Name = '{user.Username}'", conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    roles.Add(reader["AutorisatieName"].ToString());
                }
                reader.Close();
                conn.Close();
            }

            return roles;
        }

        public int GetUserID(User user)
        {
            int userId = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand($"SELECT TOP 1 Id, InlogName,Wachtwoord from dbo.Gebruiker WHERE InlogName = '{user.Username}' and Wachtwoord = '{user.Password}'", conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    userId = (int) reader["Id"];
                }
                reader.Close();
                conn.Close();
            }

            return userId;
        }
    }
}
