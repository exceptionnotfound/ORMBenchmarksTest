using ORMBenchmarksTest.DTOs;
using ORMBenchmarksTest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ORMBenchmarksTest.DataAccess
{
    public class EntityFrameworkCore : ITestSignature
    {
        public long GetPlayerByID(int id)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SportContextEfCore context = new SportContextEfCore())
            {
                var player = context.Players.Find(id);
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetPlayersForTeam(int teamId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SportContextEfCore context = new SportContextEfCore())
            {
                var players = context.Players.AsNoTracking().Where(x => x.TeamId == teamId).ToList();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetTeamsForSport(int sportId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SportContextEfCore context = new SportContextEfCore())
            {
                var players = context.Teams.AsNoTracking().Include(x=>x.Players).Where(x => x.SportId == sportId).ToList();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
    }
}
