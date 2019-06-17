using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using FHICTDeploymentSystem.Models;

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
                cmd.Parameters.Add(new SqlParameter("@TeamId", termExec.TeamId));
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

                cmd.Parameters.Add(new SqlParameter("@TermExecutionId", termExec.Id));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void EditTermExec(EducationObject termExec)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = new SqlCommand("AddTermExec", connection) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add(new SqlParameter("@TermExecutionId", termExec.Id));
                cmd.Parameters.Add(new SqlParameter("@Name", termExec.Name));
                cmd.Parameters.Add(new SqlParameter("@UnitId", termExec.UnitId));
                cmd.Parameters.Add(new SqlParameter("@EstimatedClasses", termExec.Factor));
                cmd.Parameters.Add(new SqlParameter("@TermExecutionId", termExec.Id));
                cmd.Parameters.Add(new SqlParameter("@TeamId", termExec.TeamId));
                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
