﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;

namespace DAL.Contexts
{
    public class AlgorithmContext
    {
        private readonly string _connectionString =
            "Server=mssql.fhict.local;Database=dbi389621;User Id=dbi389621;Password=Ensar123;";

        public List<EducationObject> GetAllTasks()
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
                        EstimatedHours = (int)reader["Hours"],
                        Factor = (int)reader["Factor"],
                        Period = (int)reader["Period"]
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
                    var openHoursP1 = (int) reader["HoursPeriod1"];
                    var openHoursP2 = (int) reader["HoursPeriod2"];

                    employees.Add(new Employee
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Abbreviation"],
                        OpenHours = new int[] { openHoursP1, openHoursP2 },
                        MaxOvertime = new int[] { (int)(openHoursP1 * 0.2), (int)(openHoursP2 * 0.2)}
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
                        Task = new EducationObject { Id = (reader["TaskId"] as int?).GetValueOrDefault() },
                        Value = (int)reader["Priority"],
                    });
                }

                connection.Close();
            }

            return preferences;
        }

        public IEnumerable<EducationObject> GetAllAssignedTasks()
        {
            var assignedTasks = new List<EducationObject>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllAssignedTasks", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    assignedTasks.Add(new EducationObject
                    {
                        Id = (int)reader["TaskId"]
                    });
                }

                connection.Close();
            }

            return assignedTasks;
        }

        public void AssignTask(EducationObject task, Employee emp)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand($"AssignTask", connection) { CommandType = CommandType.StoredProcedure };

                sqlCommand.Parameters.AddWithValue("@EmployeeId", emp.Id);
                sqlCommand.Parameters.AddWithValue("@TaskId", task.Id);
                sqlCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        public IEnumerable<EducationObject> GetAssignedTasksFromEmployee(Employee emp)
        {
            var assignedTasks = new List<EducationObject>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAssignedTasksFromEmployee", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeId", emp.Id);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    assignedTasks.Add(new EducationObject
                    {
                        Id = (int)reader["TaskId"],
                        Name = (string)reader["Code"],
                        Period =  (int)reader["Period"],
                        EstimatedHours = (int)reader["EstimatedHours"]
                    });
                }

                connection.Close();
            }

            return assignedTasks;
        }

        public IEnumerable<EducationObject> GetLeftOverTasks()
        {
            var assignedTasks = new List<EducationObject>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetLeftOverTasks", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    assignedTasks.Add(new EducationObject
                    {
                        Id = (int)reader["TaskId"],
                        Name = (string)reader["Code"],
                        Period = (int)reader["Period"],
                        Description = reader["Description"]?.ToString(),
                        EstimatedHours = (int)reader["EstimatedHours"],
                        Factor = (int)reader["LeftOverCount"]
                    });
                }

                connection.Close();
            }

            return assignedTasks;
        }
    }
}