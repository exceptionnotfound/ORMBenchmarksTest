using ORMBenchmarksTest.DTOs;
using ORMBenchmarksTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ORMBenchmarksTest.TestData
{
    public static class Database
    {
        public static void Reset()
        {
            using(SportContext context = new SportContext())
            {
                context.Database.ExecuteSqlCommand("DELETE FROM Player");
                context.Database.ExecuteSqlCommand("DELETE FROM Team");
                context.Database.ExecuteSqlCommand("DELETE FROM Sport");
            }
        }

        public static void Load(List<SportDTO> sports, List<TeamDTO> teams, List<PlayerDTO> players)
        {
            AddSports(sports);
            AddTeams(teams);
            AddPlayers(players);
        }

        private static void AddPlayers(List<PlayerDTO> players)
        {
            using (SportContext context = new SportContext())
            {
                foreach (var player in players)
                {
                    context.Players.Add(new Player()
                    {
                        FirstName = player.FirstName,
                        LastName = player.LastName,
                        DateOfBirth = player.DateOfBirth,
                        TeamId = player.TeamId,
                        Id = player.Id
                    });
                }

                context.SaveChanges();
            }
        }

        private static void AddTeams(List<TeamDTO> teams)
        {
            using (SportContext context = new SportContext())
            {
                foreach (var team in teams)
                {
                    context.Teams.Add(new Team()
                    {
                        Name = team.Name,
                        Id = team.Id,
                        SportId = team.SportId,
                        FoundingDate = team.FoundingDate
                    });
                }

                context.SaveChanges();
            }
        }

        private static void AddSports(List<SportDTO> sports)
        {
            using (SportContext context = new SportContext())
            {
                foreach (var sport in sports)
                {
                    context.Sports.Add(new Sport()
                    {
                        Id = sport.Id,
                        Name = sport.Name
                    });
                }

                context.SaveChanges();
            }
        }
    }
}
