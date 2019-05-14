using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using FHICTDeploymentSystem.Models;

namespace DAL.Contexts
{
    public class AddTaskContext : IAddTaskContext
    {
        private readonly string _connectionString =
            "Server=mssql.fhict.local;Database=dbi389621;User Id=dbi389621;Password=Ensar123;";

        public void AddTask(EducationObject toAddTask)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = new SqlCommand("AddTask", connection) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add(new SqlParameter("@Period", toAddTask.Period));
                cmd.Parameters.Add(new SqlParameter("@Code", toAddTask.Name));
                cmd.Parameters.Add(new SqlParameter("@Explanation", toAddTask.Explanation));
                cmd.Parameters.Add(new SqlParameter("@UnitExecId", toAddTask.UnitExecId));
                cmd.Parameters.Add(new SqlParameter("@Description", toAddTask.Description));
                cmd.Parameters.Add(new SqlParameter("@Hours", toAddTask.EstimatedHours));

                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public IEnumerable<EducationObject> GetUnitTermExecutions(int unitId)
        {
            var unitTermExecutions = new List<EducationObject>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllUnitTermExecutions", connection) {CommandType = CommandType.StoredProcedure};

                cmd.Parameters.Add(new SqlParameter("@UnitId", unitId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    unitTermExecutions.Add(new EducationObject
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"]?.ToString()
                    });
                }

                connection.Close();
            }

            return unitTermExecutions;
        }
    }
}
