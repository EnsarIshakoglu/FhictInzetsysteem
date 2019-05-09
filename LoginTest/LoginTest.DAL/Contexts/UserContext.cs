using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using FHICTDeploymentSystem.Models;

namespace FHICTDeploymentSystem.DAL
{
    public class UserContext : IUserContext
    {
        private readonly string _connectionString =
            "Server=mssql.fhict.local;Database=dbi389621;User Id=dbi389621;Password=Ensar123;";

        public bool Login(User user)
        {
            bool loginSuccessful = false;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand($"SELECT TOP 1 Id, Username,Password from dbo.Employee WHERE Username = '{user.Username}' and Password = '{user.Password}'", conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    loginSuccessful = true;
                }
                reader.Close();
                conn.Close();
            }
            return loginSuccessful;
        }

        public IEnumerable<string> GetUserRoles(User user)
        {
            var roles = new List<string>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand($"select E.Name, A.Name \r\nfrom dbo.[Emp_Auth] as EA\r\ninner join Employee E on EA.EmployeeId = E.ID\r\ninner join [Authorization] A on EA.AuthorizationId = A.ID where E.Name = '{user.Username}'", conn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    roles.Add(reader["Name"].ToString());
                }
                reader.Close();
                conn.Close();
            }

            return roles;
        }

        public int GetUserId(User user)
        {
            int userId = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand($"SELECT TOP 1 Id, Username,Password from dbo.Employee WHERE Username = '{user.Username}' and Password = '{user.Password}'", conn);
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
