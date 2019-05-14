﻿using System;
using System.Collections.Generic;
using System.Data;
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
            var loginSuccessful = false;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var command = new SqlCommand("GetUserData", conn) {CommandType = CommandType.StoredProcedure};

                command.Parameters.Add(new SqlParameter("@Username", user.Username));
                command.Parameters.Add(new SqlParameter("@Password", user.Password));

                var reader = command.ExecuteReader();

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

                var command = new SqlCommand("GetUserRoles", conn) { CommandType = CommandType.StoredProcedure };

                command.Parameters.Add(new SqlParameter("@Username", user.Username));

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    roles.Add(reader["AuthName"].ToString());
                }
                reader.Close();
                conn.Close();
            }

            return roles;
        }

        public int GetUserId(User user)
        {
            var userId = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var command = new SqlCommand("GetUserData", conn) { CommandType = CommandType.StoredProcedure };

                command.Parameters.Add(new SqlParameter("@Username", user.Username));
                command.Parameters.Add(new SqlParameter("@Password", user.Password));

                var reader = command.ExecuteReader();

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
