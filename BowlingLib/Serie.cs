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
        public int PlayRound(int turnCounter = 10)
        {
            var result = 0;
            var scoreBoard = new ScoreBoard();
            if (turnCounter > 9)
            {
              var point = CalculateSpecialPoint(RollBall(),RollBall());
            }
            else
            {

            }

            return result;
        }

        public int RollBall()
        {
            var random = new Random();
            var point = random.Next(0,10);
            return point;
        }

        public SpecialPoints CalculateSpecialPoint(int firstPoint, int secondPoint)
        {
            var result = SpecialPoints.Regular;
            if (firstPoint == 10)
            {

            }
            if (firstPoint + secondPoint == 10)
            {
               
            }
            return result;
        }

        public enum SpecialPoints
        {
            Strike,
            Spare,
            Regular
        }

        internal class ScoreBoard
        {
            public int PartyId { get; set; }
            public SpecialPoints SpecialPoints { get; set; }
            public int[,] Score { get; set; } = new int[2,9];
            public int[] LastScore { get; set; } = new int[3];
        }
    }
}
