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
    public class EntityFrameworkReusingWithQueryContext : ITestSignature, IDisposable
    {
        private readonly SportContext _context;

        public EntityFrameworkReusingWithQueryContext()
        {
            _context = new SportContext();
        }

        public long GetPlayerByID(int id)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            {
                var player = _context.Players.SqlQuery(@"SELECT Id, FirstName, LastName, DateOfBirth, TeamId FROM Player WHERE Id = @p0", id).Single();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetPlayersForTeam(int teamId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            {
                var players = _context.Players.SqlQuery(@"SELECT Id, FirstName, LastName, DateOfBirth, TeamId FROM Player WHERE TeamId = @p0", teamId).ToList();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetTeamsForSport(int sportId)
        {
            return 0;
            /*
            Stopwatch watch = new Stopwatch();
            watch.Start();
            {
                var players = _context.Players.AsNoTracking().Where(x => x.Team.SportId == sportId).ToList();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
            */
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
