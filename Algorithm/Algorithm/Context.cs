using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class Context
    {
        private readonly string _connectionString =
            "Server=mssql.fhict.local;Database=dbi389621;User Id=dbi389621;Password=Ensar123;";

        public IEnumerable<EducationObject> GetAllTasks(EducationObject section)
        {
            var tasks = new List<EducationObject>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetTasksWithinSection", connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@SectionId", section.Id));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new EducationObject
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Code"]?.ToString(),
                        EducationType = EducationType.Task
                    });
                }

                connection.Close();
            }

            return tasks;
        }
    }
}
