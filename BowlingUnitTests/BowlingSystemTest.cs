using AccountabilityLib;
using DatabaseRepoLib.Classes;
using System;
using Xunit;
using BowlingLib;
using BowlingLib.Service;
using MeasurementLib;
using System.Linq;
using static DatabaseRepoLib.Classes.DataBaseRepo;
using AccountabilityLib.Classes;

namespace BowlingUnitTests
{
    public class BowlingSystemTest
    {
        [Fact]
        public void CreateANewParty()
        {
            var party = new Party { LegalId = "0000000000", Address = "Vägen 1", IsManager = true, Name = "Manager Managersson" };
            var bowlingSystem = new BowlingSystem();
            var newParty = bowlingSystem.CreateANewManager(party.LegalId, party.Name, party.Address);
            party.PartyId = newParty.PartyId;
            Assert.Equal(party, newParty);
        }

        [Fact]
        public void CreateAContest()
        {
            var bowlingSystem = new BowlingSystem();
            var arr = new int[] { 2026, 3002, 3003, 3004 };
            bowlingSystem.CreateANewContest(arr, 4002, 1, 1);
        }

        [Fact]
        public void GetAUnitByName()
        {
            MeasurementService sut = new MeasurementService();
            var result = sut.WhatUnitDoYouNeedBro("SpElare");
            var unit = new Unit { Name = "Spelare" };
            Assert.Equal(unit.Name, result.Name);
        }

        [Fact]
        public void GetWinner()
        {
            var database = new DataBaseRepo();
            var sut = new BowlingSystem();
            var contest = (Contest)database.GetObject("1011", new Contest());
            contest.WinnerId = 2026;
            var result = sut.GetWinnerOfContest(contest);
            Assert.Equal(contest.WinnerId, result.WinnerId);
        }
        //[Fact]
        //public void PlayRoundTest()
        //{
        //    var score = new Score { LaneId=5, QuantityId=8, SerieId=3, UnitId=2  };
        //    DataBaseRepo database = new DataBaseRepo();
        //    var result = (DatabaseHolder)database.Save(score);
        //    var sut = new Serie();
        //    sut.PlayRound(result.PrimaryKey);
        //}
    }
}
