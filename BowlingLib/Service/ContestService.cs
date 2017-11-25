using AccountabilityLib.Classes;
using DatabaseRepoLib.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeasurementLib;

namespace BowlingLib.Service
{
    class ContestService
    {
        public int GetWinnerOfYear(int year)
        {
            var database = new DataBaseRepo();
            var contestList = database.GetAll(new Contest())
                .Cast<Contest>()
                .Where(c => year == database.GetAll(new TimePeriod())
                .Cast<TimePeriod>()
                .First(t => year == t.StartDate.Year && year == t.EndDate.Year).StartDate.Year)//Ska denna kunna bara kolla startdate eller enddate? Ifall dom inte stämmer överrens då smäller det...Nice to have-kategorin går detta problem under.
                .ToList();

            List<int> winnerList = new List<int>();
            foreach (var contest in contestList)
            {
                var contestWinner = GetWinnerOfContest(contest);
                winnerList.Add(contest.WinnerId);
            }

            var RemainingWinners = winnerList.GroupBy(i => i);
            var count = 0;
            var winnerId = 0;
            foreach (var item in RemainingWinners)
            {
                if (item.Count() > count)
                {
                    count = item.Count();
                    winnerId = item.Key;
                }
            }
            return winnerId;
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
    }
}
