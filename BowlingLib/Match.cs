using System;
using AccountabilityLib;
using System.Collections.Generic;
using AccountabilityLib.Classes;

namespace BowlingLib
{
    public class Match
    {
        public int MatchId { get; set; }
        public int ContestTypeId { get; set; }
        public List<Party> Competing { get; set; }
        public int LaneId { get; set; }
        public Contest Contest { get; set; }
        public Lane Lane { get; set; }

        public Party CalculateWinner(Lane lane)
        {
            return new Party();
        }

        public Lane CreateLane()
        {
            return new Lane();
        }
    }   
}
