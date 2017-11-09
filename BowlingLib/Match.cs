using System;
using AccountabilityLib;
using System.Collections.Generic;
using AccountabilityLib.Classes;
using static DatabaseRepoLib.Classes.DataBaseRepo;
using DatabaseRepoLib.Classes;
using MeasurementLib;
using DatabaseRepoLib.Interfaces;

namespace BowlingLib
{
    public class Match
    {
        public int MatchId { get; set; }
        public int ContestId { get; set; }
        //public List<Party> Competing { get; set; }        
        public int QuantityId { get; set; }
        public int UnitId { get; set; }
        public int LaneId { get; set; }
        //public Contest Contest { get; set; }
        //public Lane Lane { get; set; }

        public Party CalculateWinner(Lane lane)
        {
            return new Party();
        }

        public void CreateLanes(List<int> competitorsId, int contestId, Match match)
        {
            var database = new DataBaseRepo();
            var listOfUnitIds = database.GetAll(new Unit());
            int unitId = 0;

            foreach (Unit unit in listOfUnitIds)
            {
                if (unit.Name.ToLower() == "spelare")
                {
                    unitId = unit.UnitId;
                }
            }
            //TODO Bygga upp serier genom lane, även få med båda spelarnas ID:n så att det skapas en separat serie per spelare.
            var quantity = (DatabaseHolder)database.Save
                (new Quantity
                {
                    Amount = 2,
                    UnitId = unitId
                });

            if (quantity.ExecuteCodes == ExecuteCodes.SuccessToExecute)
            {
                var lane = new Lane
                {
                    UnitId = unitId,
                    QuantityId = quantity.PrimaryKey
                };

                var primaryKeyLane = (DatabaseHolder)database.Save(lane);
                match.LaneId = primaryKeyLane.PrimaryKey;
                match.QuantityId = quantity.PrimaryKey;
                match.UnitId = unitId;
                var matchId = (DatabaseHolder)database.Save(match);
                lane.MatchId = matchId.PrimaryKey;
                if (matchId.ExecuteCodes != ExecuteCodes.FailedToExecute)
                {
                    lane.CreateSerie(primaryKeyLane.PrimaryKey, competitorsId);
                }
            }            
        }
    }
}
