using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace LoginTest.DAL
{
    public class UserContext : IUserContext
    {
        private readonly string connectionString =
            "Data Source=(LocalDb)\\LoginTestProftaak;Initial Catalog=ProftaakTestDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool Login(string userName, string password)
        {
            bool loginSuccesfull = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand($"SELECT TOP 1 Username,Password from dbo.UserInfo WHERE Username = '{userName}' and Password = '{password}'", conn);
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
