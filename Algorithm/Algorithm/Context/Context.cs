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

        public IEnumerable<EducationObject> GetAllTasks()
        {
            var tasks = new List<EducationObject>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllTasks", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new EducationObject
                    {
                        Id = (int)reader["Id"],
                        UnitExecId = (int)reader["UnitExecId"],
                        UnitId = (int)reader["UnitId"],
                        SectionId = (int)reader["SectionId"],
                        EstimatedHours = (int)reader["Hours"],
                        Factor = (int)reader["Factor"]
                    });
                }

                connection.Close();
            }

            return tasks;
        }


        public IEnumerable<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllEmployees", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    employees.Add(new Employee
                    {
                        Id = (int)reader["Id"],
                        HoursP1 = (int)reader["HoursPeriod1"],
                        HoursP2 = (int)reader["HoursPeriod2"]
                    });
                }

                connection.Close();
            }

            return employees;
        }

        public IEnumerable<EducationObject> GetEmployeeCompetences(int employeeId)
        {
            var competences = new List<EducationObject>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetEmployeeCompetences", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", employeeId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    competences.Add(new EducationObject()
                    {
                        Id = (reader["TaskId"] as int?).GetValueOrDefault(),
                    });
                }

                connection.Close();
            }

            return competences;
        }

        public IEnumerable<Preference> GetEmployeePreferences(int employeeId)
        {
            var preferences = new List<Preference>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetEmployeePreferences", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", employeeId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    preferences.Add(new Preference
                    {
                        Task = new EducationObject{Id = (reader["TaskId"] as int?).GetValueOrDefault()},
                        Value = (int)reader["Priority"],
                    });
                }

                connection.Close();
            }

            return preferences;
        }
    }
}