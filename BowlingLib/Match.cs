using System;
using AccountabilityLib;
using System.Collections.Generic;
using AccountabilityLib.Classes;
using static DatabaseRepoLib.Classes.DataBaseRepo;
using DatabaseRepoLib.Classes;
using MeasurementLib;

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

        public void CreateLanes(int competitorId, int contestId, Match match, int turncounter, int loopLength)
        {
            var database = new DataBaseRepo();
            var listOfUnitIds = database.GetAll(new Unit());
            var unitId = new object();

            foreach (var unit in listOfUnitIds)
            {
                if (unit.GetType().GetProperty("Name").ToString().ToLower() == "spelare")
                {
                    unitId = unit;
                }
            }
            //TODO Bygga upp serier genom lane, även få med båda spelarnas ID:n så att det skapas en separat serie per spelare.
            var quantity = (DatabaseHolder)database.Save
                (new Quantity
                {
                    Amount = 2,
                    UnitId = int.Parse(unitId.GetType().GetProperty("UnitId").GetValue(unitId).ToString())
                });

            if (turncounter % 2 == 0)
            {
                var lane = new Lane
                {
                    UnitId = int.Parse(unitId.GetType().GetProperty("UnitId").GetValue(unitId).ToString()),
                    QuantityId = quantity.PrimaryKey,
                    MatchId = match.MatchId
                };
                var primaryKeyLane = (DatabaseHolder)database.Save(lane);
                match.LaneId = primaryKeyLane.PrimaryKey;
                lane.CreateSerie(primaryKeyLane.PrimaryKey);
            }

            if (turncounter % 2 == 1 && turncounter == loopLength)
            {
                var oddQuantity = (DatabaseHolder)database.Save
                (new Quantity
                {
                    Amount = 1,
                    UnitId = int.Parse(unitId.GetType().GetProperty("UnitId").GetValue(unitId).ToString())
                });
                var lane = new Lane
                {
                    UnitId = int.Parse(unitId.GetType().GetProperty("UnitId").GetValue(unitId).ToString()),
                    QuantityId = oddQuantity.PrimaryKey,
                    MatchId = match.MatchId
                };
                var primaryKeyLane = (DatabaseHolder)database.Save(lane);
                match.LaneId = primaryKeyLane.PrimaryKey;
                lane.CreateSerie(primaryKeyLane.PrimaryKey);
            }
        }
    }
}
