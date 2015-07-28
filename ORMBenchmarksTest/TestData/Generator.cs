using ORMBenchmarksTest.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMBenchmarksTest.TestData
{
    public static class Generator
    {
        public static List<PlayerDTO> GeneratePlayers(int teamId, int count)
        {
            List<PlayerDTO> players = new List<PlayerDTO>();

            var allFirstNames = Names.GetFirstNames();
            var allLastNames = Names.GetLastNames();
            Random rand = new Random();
            DateTime start = new DateTime(1975, 1, 1);
            DateTime end = new DateTime(1998, 1, 1);

            for(int i = 0; i < count; i++)
            {
                PlayerDTO player = new PlayerDTO();
                int newFirst = rand.Next(0, allFirstNames.Count - 1);
                player.FirstName = allFirstNames[newFirst];
                int newLast = rand.Next(0, allLastNames.Count - 1);
                player.LastName = allLastNames[newLast];
                player.DateOfBirth = RandomDay(rand, start, end);
                player.TeamId = teamId;
                player.Id = (((teamId - 1) * count) + (i + 1));
                players.Add(player);
            }

            return players;
        }

        public static List<TeamDTO> GenerateTeams(int sportId, int count)
        {
            List<TeamDTO> teams = new List<TeamDTO>();

            var allCityNames = Names.GetCityNames();
            var allTeamNames = Names.GetTeamNames();
            Random rand = new Random();
            DateTime start = new DateTime(1900, 1, 1);
            DateTime end = new DateTime(2010, 1, 1);

            for (int i = 0; i < count; i++)
            {
                TeamDTO team = new TeamDTO();
                int newCity = rand.Next(0, allCityNames.Count - 1);
                int newTeam = rand.Next(0, allTeamNames.Count - 1);
                team.Name = allCityNames[newCity] + " " + allTeamNames[newTeam];
                team.FoundingDate = RandomDay(rand, start, end);
                team.SportId = sportId;
                team.Id = (((sportId - 1) * count) + (i + 1));
                teams.Add(team);
            }

            return teams;
        }

        public static List<SportDTO> GenerateSports(int count)
        {
            List<SportDTO> sports = new List<SportDTO>();
            var allSportNames = Names.GetSportNames();
            Random rand = new Random();

            for(int i = 0; i < count; i++)
            {
                int newSport = rand.Next(0, allSportNames.Count - 1);
                sports.Add(new SportDTO() {
                    Name = allSportNames[newSport],
                    Id = i + 1
                });
            }

            return sports;
        }

        private static DateTime RandomDay(Random rand, DateTime start, DateTime end)
        {
            int range = (end - start).Days;
            return start.AddDays(rand.Next(range));
        }
    }
}
