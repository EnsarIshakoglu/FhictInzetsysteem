using System;
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

        public IEnumerable<User> GetAllEmployeesWithCompetenceForTask(int taskId)
        {
            var users = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllEmployeesWithCompetenceForTask", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@TaskId", taskId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Abbreviation"],
                        IsCompetentForTask = (int)reader["Competency"] != 0
                    });
                }

                connection.Close();
            }

            return users;
        }

        public void FixateTask(int taskId, int empId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = new SqlCommand("FixateTask", connection) { CommandType = CommandType.StoredProcedure };

                cmd.Parameters.Add(new SqlParameter("@TaskId", taskId));
                cmd.Parameters.Add(new SqlParameter("@EmployeeId", empId));

                cmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        public IEnumerable<EducationObject> GetAllLeftOverTasksFromUnitExecId(int unitExecId)
        {
            var tasks = new List<EducationObject>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllLeftOverTasksFromUnitExecId", connection) { CommandType = CommandType.StoredProcedure, };

                cmd.Parameters.Add(new SqlParameter("@UnitExecId", unitExecId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tasks.Add(new EducationObject
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"]?.ToString(),
                        Period = (int)reader["Period"],
                        Description = reader["Description"]?.ToString(),
                        EducationType = EducationType.UnitExec
                    });
                }

                connection.Close();
            }

            return tasks;
        }
        public IEnumerable<EducationObject> GetAllLeftOverUnitsFromSection(int sectionId)
        {
            var units = new List<EducationObject>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllLeftOverUnitsFromSection", connection) { CommandType = CommandType.StoredProcedure, };

                cmd.Parameters.Add(new SqlParameter("@SectionId", sectionId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    units.Add(new EducationObject
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"]?.ToString(),
                        EducationType = EducationType.Unit
                    });
                }

                connection.Close();
            }

            return units;
        }

        public IEnumerable<EducationObject> GetAllLeftOverUnitTermExecsFromUnit(int unitId)
        {
            var units = new List<EducationObject>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllLeftOverUnitTermExecsFromUnit", connection) { CommandType = CommandType.StoredProcedure, };

                cmd.Parameters.Add(new SqlParameter("@UnitId", unitId));

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    units.Add(new EducationObject
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"]?.ToString(),
                        EducationType = EducationType.UnitExec
                    });
                }

                connection.Close();
            }

            return units;
        }
        public IEnumerable<EducationObject> GetAllLeftOverSections()
        {
            var units = new List<EducationObject>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("GetAllLeftOverSections", connection) { CommandType = CommandType.StoredProcedure, };

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    units.Add(new EducationObject
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"]?.ToString(),
                        EducationType = EducationType.Section
                    });
                }

                connection.Close();
            }

            return units;
        }
    }
}
