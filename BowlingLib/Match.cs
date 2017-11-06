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

        public Match()
        {

        }

        public Party CalculateWinner(Lane lane)
        {
            return new Party();
        }

        public List<Lane> CreateLanes(int[] competitors, Match match)
        {
            var lanes = new List<Lane>();
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

            var evenCompetiorsListOrNot = competitors.Length % 2;

            switch (evenCompetiorsListOrNot)
            {
                case 0:
                    for (int i = 0; i < competitors.Length / 2; i++)
                    {
                        var quantity = (DatabaseHolder)database.Save(new Quantity { Amount = 2, UnitId = int.Parse(unitId.GetType().GetProperty("UnitId").GetValue(unitId).ToString()) });
                        var lane = new Lane
                        {
                            UnitId = int.Parse(unitId.GetType().GetProperty("UnitId").GetValue(unitId).ToString()),
                            QuantityId = quantity.PrimaryKey,
                            MatchId = match.MatchId
                        };

                    }

                    //TODO Bygga upp serier genom lane.
                    break;
                case 1:
                    break;
                default:
                    break;
            }
            
            return lanes;
        }
    }   
}
