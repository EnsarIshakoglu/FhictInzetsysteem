﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Models;
using Models.Enums;

namespace DAL.Contexts
{
    public class TaskContext : ITaskContext
    {
        private readonly string _connectionString =
            "Server=mssql.fhict.local;Database=dbi389621;User Id=dbi389621;Password=Ensar123;";

        public void AddTask(EducationObject toAddTask)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = new SqlCommand("AddTask", connection) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add(new SqlParameter("@Period", toAddTask.Period));
                cmd.Parameters.Add(new SqlParameter("@Code", toAddTask.Name));
                cmd.Parameters.Add(new SqlParameter("@Explanation", toAddTask.Explanation));
                cmd.Parameters.Add(new SqlParameter("@UnitExecId", toAddTask.UnitExecId));
                cmd.Parameters.Add(new SqlParameter("@Description", toAddTask.Description));
                cmd.Parameters.Add(new SqlParameter("@Hours", toAddTask.EstimatedHours));
                cmd.Parameters.Add(new SqlParameter("@Factor", toAddTask.Factor));

                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public void RemoveTask(EducationObject toRemoveTask)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = new SqlCommand("RemoveTask", connection) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add(new SqlParameter("@TaskId", toRemoveTask.Id));
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

                SqlCommand cmd = new SqlCommand("GetAllUnitTermExecutions", connection) { CommandType = CommandType.StoredProcedure, };

                cmd.Parameters.Add(new SqlParameter("@UnitId", unitId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    unitTermExecutions.Add(new EducationObject
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"]?.ToString(),
                        TermExecution = reader["TermExecution"]?.ToString(),
                        EducationType = EducationType.UnitExec
                    });
                }

                connection.Close();
            }

            return unitTermExecutions;
        }

        public void UpdateTask(EducationObject task)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = new SqlCommand("UpdateTask", connection) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add(new SqlParameter("@TaskId", task.Id));
                cmd.Parameters.Add(new SqlParameter("@Period", task.Period));
                cmd.Parameters.Add(new SqlParameter("@Code", task.Name));
                cmd.Parameters.Add(new SqlParameter("@Explanation", task.Explanation));
                cmd.Parameters.Add(new SqlParameter("@Description", task.Description));
                cmd.Parameters.Add(new SqlParameter("@Hours", task.EstimatedHours));

                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public EducationObject GetTaskById(EducationObject task)
        {
            var returnValue = new EducationObject();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetTaskFromId", connection) { CommandType = CommandType.StoredProcedure, };

                cmd.Parameters.Add(new SqlParameter("@TaskId", task.Id));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    returnValue = new EducationObject
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Code"]?.ToString(),
                        Description = reader["Description"]?.ToString(),
                        EducationType = EducationType.Task,
                        Factor = (int)reader["Factor"],
                        EstimatedHours = (int)reader["Hours"],
                        Period = (int)reader["Period"],
                        Explanation = reader["Explanation"]?.ToString()
                    };
                }
                reader.Close();
                connection.Close();
            }

            return returnValue;
        }

        public IEnumerable<EducationObject> GetEmployeeAssignedTasks(int empId)
        {
            var assignedTasks = new List<EducationObject>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetEmployeeAssignedTasks", connection) { CommandType = CommandType.StoredProcedure, };

                cmd.Parameters.Add(new SqlParameter("@EmployeeId", empId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    assignedTasks.Add(new EducationObject
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Code"]?.ToString(),
                        Description = reader["Description"]?.ToString(),
                        EducationType = EducationType.Task,
                        Factor = (int)reader["Factor"],
                        EstimatedHours = (int)reader["Hours"],
                        Period = (int)reader["Period"],
                        Explanation = reader["Explanation"]?.ToString()
                    });
                }
                reader.Close();
                connection.Close();
            }

            return assignedTasks;
        }
    }
}
