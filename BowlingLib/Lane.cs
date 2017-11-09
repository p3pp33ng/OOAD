using DatabaseRepoLib.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using static DatabaseRepoLib.Classes.DataBaseRepo;

namespace BowlingLib
{
    public class Lane
    {
        public int LaneId { get; set; }
        //public int ScoreId { get; set; }
        //public int MatchId { get; set; }
        public int QuantityId { get; set; }
        public int UnitId { get; set; }
        //public Match Match { get; set; }
        //public Score Score { get; set; }
        //public List<Serie> Series { get; set; }

        public void CreateSerie(int laneId, List<int> compIds)
        {
            var database = new DataBaseRepo();
            for (int i = 0; i < compIds.Count; i++)
            {
                var score = new Score { LaneId = laneId };
                var serie = new Serie { LaneId = laneId, PartyId = compIds[i], TurnCounter = 10 };
                var dataHolderScore = (DatabaseHolder)database.Save(score);
                serie.ScoreId = dataHolderScore.PrimaryKey;
                var dataHolderSerie = (DatabaseHolder)database.Save(serie);
                score.SerieId = dataHolderSerie.PrimaryKey;
            }
        }
    }
}
