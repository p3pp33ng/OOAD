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
        public int ScoreId { get; set; }
        //public Party Party { get; set; }
        //public Score Score { get; set; }
        //public Lane Lane { get; set; }

        //TODO Fixa dessa metoder
        public void PlayRound()
        {
            
        }

        public int RollBall()
        {
            var random = new Random();
            var point = random.Next(0,10);
            return point;
        }

        public int CalculateSpecialPoint(int firstPoint, int secondPoint)
        {
            if (firstPoint == 10)
            {

            }
            if (firstPoint + secondPoint == 10)
            {
               
            }
            return 1;
        }

        internal class ScoreBoard
        {
            public int PartyId { get; set; }
            public int[,] Score { get; set; } = new int[2,9];
            public int[] LastScore { get; set; } = new int[3];
        }
    }
}
