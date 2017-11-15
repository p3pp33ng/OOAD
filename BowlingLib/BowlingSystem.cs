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
        public void GetWInnerOfTheYear(int year)
        {
            var database = new DataBaseRepo();
            var contestList = database.GetAll(new Contest())
                .Cast<Contest>()
                .Where(c => year == database.GetAll(new TimePeriod()).Cast<TimePeriod>().First(t => year == t.StartDate.Year && year == t.EndDate.Year).StartDate.Year)//TODO Måste ändra denna linq så att den söker på contest Timeperiod inte på alla timeperiods.
                .ToList();
            List<int> winnerList = new List<int>();
            foreach (var contest in contestList)
            {
                var contestWinner = GetWinnerOfContest(contest);
                winnerList.Add(contest.WinnerId);
            }
            winnerList.Sort();
            foreach (var winnerId in winnerList)
            {
                //TODO Kolla vilket id som dyker upp mest.
            }
        }

        public Contest GetWinnerOfContest(Contest contest)
        {
            var result = 0;
            var database = new DataBaseRepo();

            var players = database
                .GetAll(new ContestParticipants())
                .Cast<ContestParticipants>()
                .Where(p => contest.ContestId == p.ContestId)
                .ToList();

            var series = database
                .GetAll(new Serie())
                .Cast<Serie>()
                .Where(s => contest.ContestId == s.ContestId)
                .ToList();

            var scores = database.GetAll(new Score())
                .Cast<Score>()
                .ToList();

            var higherScore = 0;
            var currentScore = 0;
            foreach (var serie in series)
            {
                var listOfSerieIdsAndPartyIds = new Dictionary<int, int>();
                if (players.FirstOrDefault(p => serie.PartyId == p.CompetitorId) != null)
                {
                    listOfSerieIdsAndPartyIds.Add(serie.PartyId, serie.SerieId);
                }

                if (listOfSerieIdsAndPartyIds.Count != 0)
                {
                    foreach (var item in listOfSerieIdsAndPartyIds)
                    {
                        var quantity = (Quantity)database.GetObject(scores.First(sco => item.Value == sco.SerieId).QuantityId.ToString(), new Quantity());
                        higherScore = quantity.Amount;
                        if (higherScore > currentScore)
                        {
                            currentScore = higherScore;
                            result = item.Key;
                        }
                    }
                }
            }
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
    }
}
