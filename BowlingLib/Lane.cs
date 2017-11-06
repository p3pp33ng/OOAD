using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLib
{
    public class Lane
    {
        public int LaneId { get; set; }
        //public int ScoreId { get; set; }
        public int MatchId { get; set; }
        public int QuantityId { get; set; }
        public int UnitId { get; set; }
        //public Match Match { get; set; }
        //public Score Score { get; set; }
        //public List<Serie> Series { get; set; }

        public Serie CreateSerie(int laneId)
        {

            return new Serie();
        }
    }
}
