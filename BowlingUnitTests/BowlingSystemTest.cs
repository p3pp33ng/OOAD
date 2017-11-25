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
        public void CreateANewPlayer()
        {
            var sut = new BowlingSystem();
            var result = sut.CreateANewPlayer("8405024758", "Matts Mattsson", "Bowlinghallen");
            Assert.Equal("Bowlinghallen", result.Address);
        }

        public void CreateANewManager()
        {
            var sut = new BowlingSystem();
            var result = sut.CreateANewManager("8405024758", "Peppe peppson", "Bowlinghallen 1");
            Assert.Equal("Bowlinghallen 1", result.Address);
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
            Assert.Equal(contest.ContestId, result.ContestId);
        }
        [Fact]
        public void GetWinnerOfTheYear()
        {
            var sut = new BowlingSystem();
            var result = sut.GetWInnerOfTheYear(2017);
            Assert.Equal(0,result.PartyId);
        }
    }
}
