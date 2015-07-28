using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMBenchmarksTest.DataAccess
{
    public interface ITestSignature
    {
        long GetPlayerByID(int id);
        long GetPlayersForTeam(int teamID);
        long GetTeamsForSport(int sportID);
    }
}
