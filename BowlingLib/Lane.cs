using BowlingLib.Service;
using DatabaseRepoLib.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using static DatabaseRepoLib.Classes.DataBaseRepo;
using MeasurementLib;

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

        public void CreateSerie(int laneId, int contestId, List<int> compIds)
        {
            var measurementService = new MeasurementService();
            var unit = measurementService.WhatUnitDoYouNeedBro("poäng");
            var quantity = measurementService.CreateANewQuantity(0, unit.UnitId);
            var database = new DataBaseRepo();
            for (int i = 0; i < compIds.Count; i++)//To get three series and three scores.
            {
                for (int j = 3; j > 0; j--)
                {
                    var serie = new Serie { LaneId = laneId, PartyId = compIds[i], TurnCounter = 10, ContestId = contestId };
                    var dataHolderSerie = (DatabaseHolder)database.Save(serie);
                    var score = new Score { LaneId = laneId, UnitId = unit.UnitId, QuantityId = quantity.QuantityId, SerieId = dataHolderSerie.PrimaryKey };
                    var dataHolderScore = (DatabaseHolder)database.Save(score);
                }

                //serie.PlayRound(dataHolderScore.PrimaryKey);
            }
        }
    }
}
