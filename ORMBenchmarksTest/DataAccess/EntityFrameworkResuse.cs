﻿using ORMBenchmarksTest.DTOs;
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
    public class EntityFrameworkReusingContext : ITestSignature, IDisposable
    {
        private readonly SportContext _context;

        public EntityFrameworkReusingContext()
        {
            _context = new SportContext();
        }

        public long GetPlayerByID(int id)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            {
                var player = _context.Players.AsNoTracking().First(x => x.Id == id);
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetPlayersForTeam(int teamId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            {
                var players = _context.Players.AsNoTracking().Where(x => x.TeamId == teamId).ToList();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long GetTeamsForSport(int sportId)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            {
                var players = _context.Players.AsNoTracking().Where(x => x.Team.SportId == sportId).ToList();
            }
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
