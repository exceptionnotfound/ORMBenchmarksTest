using ORMBenchmarksTest.Configuration;
using ORMBenchmarksTest.DataAccess;
using ORMBenchmarksTest.DTOs;
using ORMBenchmarksTest.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMBenchmarksTest
{
    class Program
    {
        public static int NumPlayers { get; set; }
        public static int NumTeams { get; set; }
        public static int NumSports { get; set; }
        public static int NumRuns { get; set; }
        static void Main(string[] args)
        {
            char input;
            AutoMapperConfiguration.Configure();
            do
            {
                ShowMenu();

                input = Console.ReadLine().First();
                switch (input)
                {
                    case 'Q':
                        break;

                    case 'T':
                        List<TestResult> testResults = new List<TestResult>();
                        
                        Console.WriteLine("# of Test Runs:");
                        NumRuns = int.Parse(Console.ReadLine());

                        //Gather Details for Test
                        Console.WriteLine("# of Sports per Run: ");
                        NumSports = int.Parse(Console.ReadLine());

                        Console.WriteLine("# of Teams per Sport: ");
                        NumTeams = int.Parse(Console.ReadLine());

                        Console.WriteLine("# of Players per Team: ");
                        NumPlayers = int.Parse(Console.ReadLine());


                        List<SportDTO> sports = TestData.Generator.GenerateSports(NumSports);
                        List<TeamDTO> teams = new List<TeamDTO>();
                        List<PlayerDTO> players = new List<PlayerDTO>();
                        foreach (var sport in sports)
                        {
                            var newTeams = TestData.Generator.GenerateTeams(sport.Id, NumTeams);
                            teams.AddRange(newTeams);
                            foreach (var team in newTeams)
                            {
                                var newPlayers = TestData.Generator.GeneratePlayers(team.Id, NumPlayers);
                                players.AddRange(newPlayers);
                            }
                        }

                        Database.Reset();
                        Database.Load(sports, teams, players);

                        for (int i = 0; i < NumRuns; i++)
                        {
                            EntityFramework efTest = new EntityFramework();
                            testResults.AddRange(RunTests(i, Framework.EntityFramework, efTest));

                            EntityFrameworkCore efCoreTest = new EntityFrameworkCore();
                            testResults.AddRange(RunTests(i, Framework.EntityFrameworkCore, efCoreTest));

                            ADONET adoTest = new ADONET();
                            testResults.AddRange(RunTests(i, Framework.ADONET, adoTest));

                            ADONetReader adoReaderTest = new ADONetReader();
                            testResults.AddRange(RunTests(i, Framework.ADONetDr, adoReaderTest));

                            DataAccess.Dapper dapperTest = new DataAccess.Dapper();
                            testResults.AddRange(RunTests(i, Framework.Dapper, dapperTest));
                        }
                        ProcessResults(testResults);

                        break;
                }

            }
            while (input != 'Q');
        }


        public static List<TestResult> RunTests(int runID, Framework framework, ITestSignature testSignature)
        {
            List<TestResult> results = new List<TestResult>();

            TestResult result = new TestResult() { Run = runID, Framework = framework };
            List<long> playerByIDResults = new List<long>();
            for (int i = 1; i <= NumPlayers; i++)
            {
                playerByIDResults.Add(testSignature.GetPlayerByID(i));
            }
            result.PlayerByIDMilliseconds = Math.Round(playerByIDResults.Average(), 2);

            List<long> playersForTeamResults = new List<long>();
            for (int i = 1; i <= NumTeams; i++)
            {
                playersForTeamResults.Add(testSignature.GetPlayersForTeam(i));
            }
            result.PlayersForTeamMilliseconds = Math.Round(playersForTeamResults.Average(), 2);
            List<long> teamsForSportResults = new List<long>();
            for (int i = 1; i <= NumSports; i++)
            {
                teamsForSportResults.Add(testSignature.GetTeamsForSport(i));
            }
            result.TeamsForSportMilliseconds = Math.Round(teamsForSportResults.Average(), 2);
            results.Add(result);

            return results;
        }

        public static void ProcessResults(List<TestResult> results)
        {
            var groupedResults = results.GroupBy(x => x.Framework);
            foreach(var group in groupedResults)
            {
                Console.WriteLine(group.Key.ToString() + " Results");
                Console.WriteLine("Run #\tPlayer by ID\t\tPlayers per Team\t\tTeams per Sport");
                var orderedResults = group.OrderBy(x=>x.Run);
                foreach(var orderResult in orderedResults)
                {
                    Console.WriteLine(orderResult.Run.ToString() + "\t\t" + orderResult.PlayerByIDMilliseconds + "\t\t\t" + orderResult.PlayersForTeamMilliseconds + "\t\t\t" + orderResult.TeamsForSportMilliseconds);
                }
            }
        }

        public static void ShowMenu()
        {
            Console.WriteLine("Please enter one of the following options:");
            Console.WriteLine("Q - Quit");
            Console.WriteLine("T - Run Test");
            Console.WriteLine("Option:");
        }
    }
}
