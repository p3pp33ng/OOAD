using AccountabilityLib;
using AccountabilityLib.Classes;
using DatabaseRepoLib.Classes;
using MeasurementLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BowlingLib.Service;
using static DatabaseRepoLib.Classes.DataBaseRepo;

namespace BowlingLib
{
    public class BowlingSystem
    {
        public Party GetWInnerOfTheYear(int year)
        {
            var contestService = new ContestService();
            var database = new DataBaseRepo();
            var winnerId = contestService.GetWinnerOfYear(year);
            return (Party)database.GetObject(winnerId.ToString(), new Party());
        }

        public Contest GetWinnerOfContest(Contest contest)
        {
            var contestService = new ContestService();
            var result = contestService.GetWinnerOfContest(contest);
            return result;
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
            var contest = new Contest
            {
                ContestTypeId = contestTypeId,
                ManagerId = managerId,
                TimePeriodId = timePeriodId,
                WinnerId = 0
            };
            var databaseHolder = (DatabaseHolder)database.Save(contest);

            for (int i = 0; i < competitors.Length; i++)
            {
                ContestParticipants contestParticipants = new ContestParticipants
                {
                    ContestId = databaseHolder.PrimaryKey,
                    CompetitorId = competitors[i]
                };
                database.Save(new ContestParticipants());
                compId.Add(competitors[i]);
                if (compId.Count == 2 && i > 0)
                {

                    var match = new Match { ContestId = databaseHolder.PrimaryKey };
                    match.CreateLanes(compId, databaseHolder.PrimaryKey, match);
                    compId.Clear();
                }
                if (i % 2 == 1 && i == competitors.Length)
                {
                    databaseHolder = (DatabaseHolder)database.Save(contest);
                    var match = new Match();
                    var matchId = (DatabaseHolder)database.Save(match);
                    match.MatchId = matchId.PrimaryKey;
                    match.CreateLanes(compId, databaseHolder.PrimaryKey, match);
                    compId.Clear();
                }
            }
        }

        public List<Match> SeeMatches(int contestId)
        {
            var database = new DataBaseRepo();
            var result = database.GetAll(new Match()).Cast<Match>().ToList();
            return result.Where(m => contestId == m.ContestId).ToList();
        }

        internal class WinnerOfYearCounter
        {
            public int WinnerID { get; set; }
            public int Count { get; set; }
        }
    }
}
