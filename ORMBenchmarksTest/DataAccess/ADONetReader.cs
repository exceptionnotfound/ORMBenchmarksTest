using ORMBenchmarksTest.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMBenchmarksTest.DataAccess
{
    public class ADONetReader : ITestSignature
    {
        public long GetPlayerByID(int id)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("SELECT Id, FirstName, LastName, DateOfBirth, TeamId FROM Player WHERE Id = @ID", conn))
                {
                    command.Parameters.Add(new SqlParameter("@ID", id));
                    var reader = command.ExecuteReader();
                    var item = AutoMapper.Mapper.DynamicMap<List<PlayerDTO>>(reader);

                }
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetPlayersForTeam(int teamId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("SELECT Id, FirstName, LastName, DateOfBirth, TeamId FROM Player WHERE TeamId = @ID", conn))
                {
                    command.Parameters.Add(new SqlParameter("@ID", teamId));
                    var reader = command.ExecuteReader();
                    var items = AutoMapper.Mapper.DynamicMap<List<PlayerDTO>>(reader);
                }
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetTeamsForSport(int sportId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("SELECT p.Id, p.FirstName, p.LastName, p.DateOfBirth, p.TeamId, t.Id as TeamId, t.Name, t.SportId FROM Player p "
                                                                  + "INNER JOIN Team t ON p.TeamId = t.Id WHERE t.SportId = @ID", conn))
                {
                    command.Parameters.Add(new SqlParameter("@ID", sportId));
                    var reader = command.ExecuteReader();
                    var items = AutoMapper.Mapper.DynamicMap<List<PlayerDTO>>(reader);
                }
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}
