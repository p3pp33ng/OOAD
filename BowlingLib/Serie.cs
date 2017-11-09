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
        //public int ScoreId { get; set; }
        //public Party Party { get; set; }
        //public Score Score { get; set; }
        //public Lane Lane { get; set; }

        //TODO Fixa dessa metoder
        //public int PlayRound(int scoreId, int turnCounter = 10)
        //{
        //    var result = 0;
        //    var backlog = 0;
        //    //Lägga allt i en forloop som räknar ner från tio.
        //    var scoreBoard = new ScoreBoard();
        //    for (int i = turnCounter; i >= 0; i--)
        //    {
        //        if (i >= 1)
        //        {
        //            scoreBoard.Score[0, i - 2] = RollBall();
        //            scoreBoard.Score[1, i - 2] = RollBall();
        //            var specialPointOrNot = CalculateSpecialPoint(scoreBoard.Score[0,i-2], scoreBoard.Score[1,i-2]);
        //            switch (specialPointOrNot)
        //            {
        //                case SpecialPoints.Strike:

        //                    break;
        //                case SpecialPoints.Spare:
        //                    break;
        //                case SpecialPoints.Regular:
        //                    break;
        //                default:
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            var point = CalculateSpecialPoint(RollBall(), RollBall());
        //            if (SpecialPoints.Strike == point || SpecialPoints.Spare == point)
        //            {

        //            }
        //        }
        //    }


        //    return result;
        //}

        //public int RollBall()
        //{
        //    var random = new Random();
        //    var point = random.Next(0, 10);
        //    return point;
        //}

        //public SpecialPoints CalculateSpecialPoint(int firstPoint, int secondPoint = 0)
        //{
        //    var result = SpecialPoints.Regular;
        //    if (firstPoint == 10)
        //    {
        //        result = SpecialPoints.Strike;
        //    }
        //    if (firstPoint + secondPoint >= 10)
        //    {
        //        result = SpecialPoints.Spare;
        //    }
        //    return result;
        //}

        //public enum SpecialPoints
        //{
        //    Strike,
        //    Spare,
        //    Regular
        //}

        //internal class ScoreBoard
        //{
        //    public int PartyId { get; set; }
        //    public SpecialPoints SpecialPoints { get; set; }
        //    public int[,] Score { get; set; } = new int[2, 9];
        //    public int[] LastScore { get; set; } = new int[3];
        //}
    }
}
