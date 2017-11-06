using AccountabilityLib;
using AccountabilityLib.Classes;
using DatabaseRepoLib.Classes;
using MeasurementLib;
using System;
using System.Collections.Generic;
using System.Text;
using static DatabaseRepoLib.Classes.DataBaseRepo;

namespace BowlingLib
{
    public class BowlingSystem
    {
        public void GetWinnerOfContest()
        {
            //TODO Så man får vinnaren för det året eller match eller tävling
        }

        public Party CreateANewPlayer(string legalId, string name, string address, bool isManager = false)
        {
            var member = new Party { Address = address, IsManager = isManager, LegalId = legalId, Name = name };
            var dataBase = new DataBaseRepo();
            var databaseHolder = (DatabaseHolder)dataBase.Save(member);
            member.PartyId = databaseHolder.PrimaryKey;
            return member;
        }

        public Party CreateANewManager(string legalId, string name, string address, bool isManager = true)
        {
            var member = new Party { Address = address, IsManager = isManager, LegalId = legalId, Name = name };
            var dataBase = new DataBaseRepo();
            var databaseHolder = (DatabaseHolder)dataBase.Save(member);
            member.PartyId = databaseHolder.PrimaryKey;
            return member;
        }

        public void CreateANewContest(int[] competitors, int managerId, int timePeriodId, int contestTypeId)
        {
            var database = new DataBaseRepo();
            for (int i = 0; i < competitors.Length; i++)
            {
                var contest = new Contest
                {
                    CompetitorId = competitors[i],
                    ContestTypeId = contestTypeId,
                    ManagerId = managerId,
                    TimePeriodId = timePeriodId
                };
                var databaseHolder = (DatabaseHolder)database.Save(contest);
                var match = new Match();
                var matchId = (DatabaseHolder)database.Save(match);
                match.MatchId = matchId.PrimaryKey;
                match.CreateLanes(competitors[i], databaseHolder.PrimaryKey, match, i, competitors.Length);
            }

            //TODO Skapa matcher med två motspelare.

            //TODO Skapa en lane för varje match.

            //TODO Skapa tre serier för varje spelare.

            //TODO Spela matcherna och skapa Score för att lägg till i matcherna.
        }

        public void SeeMatches()
        {
            //TODO Retunera alla matcher, kanske lägga till så att man kan sortera på år eller liknade.
        }
    }
}
