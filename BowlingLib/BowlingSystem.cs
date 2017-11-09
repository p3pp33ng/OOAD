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
        public Contest GetWinnerOfContest(Contest contest)
        {
            var result = 0;
            var database = new DataBaseRepo();

            var series = database.GetAll(new Serie())
                .Cast<Serie>()
                .ToList();

            var players = database.GetAll(new ContestParticipants())
                .Cast<ContestParticipants>()
                .Where(c => contest.ContestId == c.ContestId)
                .ToList();

            var scores = database.GetAll(new Score())
                .Cast<Score>()
                .ToList();

            var quantities = database.GetAll(new Quantity())
                .Cast<Quantity>()
                .ToList();


            //var listOfLaneIdsAndCompetitorId = new Dictionary<int,int>();

            //foreach (ContestParticipants item in database.GetAll(new ContestParticipants()).Cast<ContestParticipants>().Where(c => contest.ContestId == c.ContestId).ToList())
            //{
            //    listOfLaneIdsAndCompetitorId.Add(series.First(s => item.CompetitorId == s.PartyId).LaneId, item.CompetitorId);
            //}



            //foreach (var laneId in listOfLaneIdsAndCompetitorId)
            //{
            //    scores.Where(s => laneId.Key == s.LaneId).ToList();
            //}

            contest.WinnerId = result;
            database.Update(contest);
            return contest;
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
                TimePeriodId = timePeriodId
            };
            var databaseHolder = (DatabaseHolder)database.Save(contest);

            for (int i = 0; i < competitors.Length; i++)
            {
                //TODO Skapa ContestParticipants istället för contest.
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
