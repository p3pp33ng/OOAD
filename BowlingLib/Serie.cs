using AccountabilityLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLib
{
    public class Serie
    {
        public int SerieId { get; set; }
        public int TurnCounter { get; set; }
        public int PartyId { get; set; }
        public int LaneId { get; set; }
        //public Party Party { get; set; }
        //public Score Score { get; set; }
        //public Lane Lane { get; set; }

        //TODO Fixa dessa metoder
        public int RollBall()
        {           
            return 1;
        }

        public int CalculateStrike(int points)
        {
            return 1;
        }

        public int CalculateSpare(int points)
        {
            return 1;
        }
    }
}
