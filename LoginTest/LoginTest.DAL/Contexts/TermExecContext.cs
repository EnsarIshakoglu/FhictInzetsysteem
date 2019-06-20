using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;
using Models.Enums;

namespace DAL.Contexts
{
    public class TermExecContext : ITermExecContext
    {
        private readonly string _connectionString =
            "Server=mssql.fhict.local;Database=dbi389621;User Id=dbi389621;Password=Ensar123;";

        public void AddTermExec(EducationObject termExec)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = new SqlCommand("AddTermExec", connection) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add(new SqlParameter("@Name", termExec.Name));
                cmd.Parameters.Add(new SqlParameter("@UnitId", termExec.UnitId));
                cmd.Parameters.Add(new SqlParameter("@EstimatedClasses", termExec.Factor));
                cmd.Parameters.Add(new SqlParameter("@TermExecutionId", termExec.Id));
                cmd.Parameters.Add(termExec.TeamId > 0 ? new SqlParameter("@TeamId", termExec.TeamId) : new SqlParameter("@TeamId", DBNull.Value));

                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void DeleteTermExec(EducationObject termExec)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = new SqlCommand("RemoveTermExec", connection) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add(new SqlParameter("@UnitTermExecId", termExec.Id));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void EditTermExec(EducationObject termExec)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = new SqlCommand("ChangeTermExec", connection) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add(new SqlParameter("@Id", termExec.UnitExecId));
                cmd.Parameters.Add(new SqlParameter("@Name", termExec.Name));
                cmd.Parameters.Add(new SqlParameter("@EstimatedClasses", termExec.Factor));
                cmd.Parameters.Add(termExec.TeamId > 0 ? new SqlParameter("@TeamId", termExec.TeamId) : new SqlParameter("@TeamId", DBNull.Value));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public IEnumerable<EducationObject> GetAllTermExecs()
        {
            var termExecs = new List<EducationObject>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllTermExecs", connection) { CommandType = CommandType.StoredProcedure, };

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    termExecs.Add(new EducationObject
                    {
                        Name = reader["Name"]?.ToString(),
                        Period = (int)reader["Season"],
                        Year = (int)reader["Year"],
                        Id = (int)reader["Id"]
                    });
                }

                connection.Close();
            }

            return termExecs;
        }

        public EducationObject GetUnitTermExecFromId(int unitTermExecId)
        {
            var unitTermExec = new EducationObject();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetUnitTermExecFromId", connection) { CommandType = CommandType.StoredProcedure, };
                cmd.Parameters.Add(new SqlParameter("@UnitTermExecId", unitTermExecId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    unitTermExec = new EducationObject
                    {
                        Name = reader["Name"]?.ToString(),
                        Factor = (int)reader["EstimatedClasses"],
                        TeamId = (reader["TeamId"] as int?).GetValueOrDefault(),
                        UnitExecId = (int)reader["Id"]
                    };
                }

                connection.Close();
            }

            return unitTermExec;
        }
    }
}
