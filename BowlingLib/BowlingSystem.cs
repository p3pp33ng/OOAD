using AccountabilityLib;
using AccountabilityLib.Classes;
using DatabaseRepoLib.Classes;
using MeasurementLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DatabaseRepoLib.Classes.DataBaseRepo;

namespace BowlingLib
{
    public class BowlingSystem
    {
        public void GetWinnerOfContest(int contestTypeId, int timeperiodId)
        {
            var database = new DataBaseRepo();
            var databaseHolder = database.GetAll(new Contest())
                .Cast<Contest>()
                .Where(c => contestTypeId == c.ContestTypeId || timeperiodId == c.TimePeriodId)
                .ToList();
            //foreach (Contest contest in database.GetAll(new Contest()).ToList())
            //{

            //}
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
            var compId = new List<int>();
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
                compId.Add(competitors[i]);
                if (compId.Count == 2 && i > 0)
                {
                    var databaseHolder = (DatabaseHolder)database.Save(contest);
                    var match = new Match { ContestId = databaseHolder.PrimaryKey };
                    match.CreateLanes(compId, databaseHolder.PrimaryKey, match);
                    compId.Clear();
                }
                if (i % 2 == 1 && i == competitors.Length)
                {
                    var databaseHolder = (DatabaseHolder)database.Save(contest);
                    var match = new Match();
                    var matchId = (DatabaseHolder)database.Save(match);
                    match.MatchId = matchId.PrimaryKey;
                    match.CreateLanes(compId, databaseHolder.PrimaryKey, match);
                    compId.Clear();
                }
            }

            //TODO Skapa matcher med två motspelare.

            //TODO Skapa en lane för varje match.

            //TODO Skapa tre serier för varje spelare.

            //TODO Spela matcherna och skapa Score för att lägg till i matcherna.
        }

        public List<Match> SeeMatches(int contestId)
        {
            var database = new DataBaseRepo();
            var result = database.GetAll(new Match()).Cast<Match>().ToList();
            return result.Where(m => contestId == m.ContestId).ToList();
        }
    }
}
