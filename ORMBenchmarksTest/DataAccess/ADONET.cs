using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMBenchmarksTest.DataAccess
{
    public class ADONET : ITestSignature
    {
        public long GetPlayerByID(int id)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using(SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                conn.Open();
                using(SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id, FirstName, LastName, DateOfBirth, TeamId FROM Player WHERE Id = @ID", conn))
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ID", id));
                    DataTable table = new DataTable();
                    adapter.Fill(table);
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
                using(SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id, FirstName, LastName, DateOfBirth, TeamId FROM Player WHERE TeamId = @ID", conn))
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ID", teamId));
                    DataTable table = new DataTable();
                    adapter.Fill(table);
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
                using(SqlDataAdapter adapter = new SqlDataAdapter("SELECT p.Id, p.FirstName, p.LastName, p.DateOfBirth, p.TeamId, t.Id as TeamId, t.Name, t.SportId FROM Player p "
                                                                  + "INNER JOIN Team t ON p.TeamId = t.Id WHERE t.SportId = @ID", conn))
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@ID", sportId));
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                }
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}
