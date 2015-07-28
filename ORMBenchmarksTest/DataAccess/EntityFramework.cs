using ORMBenchmarksTest.DTOs;
using ORMBenchmarksTest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ORMBenchmarksTest.DataAccess
{
    public class EntityFramework : ITestSignature
    {
        public long GetPlayerByID(int id)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SportContext context = new SportContext())
            {
                var player = context.Players.Where(x => x.Id == id).First();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetPlayersForTeam(int teamId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SportContext context = new SportContext())
            {
                var players = context.Players.Where(x => x.TeamId == teamId).ToList();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetTeamsForSport(int sportId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SportContext context = new SportContext())
            {
                var players = context.Teams.Include(x=>x.Players).Where(x => x.SportId == sportId).ToList();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}
