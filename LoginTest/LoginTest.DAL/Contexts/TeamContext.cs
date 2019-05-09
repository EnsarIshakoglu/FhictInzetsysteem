using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Inzetsysteem.Models;


namespace Inzetsysteem.DAL.Contexts
{
    public class TeamContext : ITeamContext
    {

        private readonly string _connectionString =
            "Server=mssql.fhict.local;Database=dbi389621;User Id=dbi389621;Password=Ensar123;";

        // vraag alle teams op(nummer en naam)
        public IEnumerable<Team> GetAllTeams()
        {
            var teams = new List<Team>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand("select ID, Naam from Team", connection);
                var reader = sqlCommand.ExecuteReader();


                while (reader.Read())
                {
                    teams.Add(new Team
                    {
                        Naam = (string) reader["Naam"],
                        Id = (int) reader["ID"]
                    });
                }

                connection.Close();
            }

            return teams;
        }

        //vraag alle users op die bij een specifiek(ID) team horen(id en naam)
        public IEnumerable<User> GetTeamUsers(int id)
        {
            var team = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var sqlCommand = new SqlCommand("GetAllTeams", connection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add(new SqlParameter("@Teamid", id));

                var reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    //voeg namen van mensen toe die team id hebben
                    team.Add(new User
                    {
                        Name = (string)reader["Naam"],
                        Id = (int)reader["ID"]
                    });
                }

                connection.Close();
            }

            return team;
        }

        public int Getid(Team team)
        {
            return team.Id;
        }

        public string Getnaam(Team team)
        {
            return team.Naam;
        }

    }
}
