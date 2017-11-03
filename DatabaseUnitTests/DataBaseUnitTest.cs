using AccountabilityLib;
using System;
using Xunit;
using DatabaseRepoLib.Classes;
using DatabaseRepoLib.Interfaces;
using AccountabilityLib.Classes;
using static DatabaseRepoLib.Classes.DataBaseRepo;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseUnitTests
{
    public class DataBaseUnitTest
    {
        [Fact]
        public void SaveToTable()
        {
            //var timePeriod = new TimePeriod { StartDate=DateTime.Now, EndDate=DateTime.Parse("2018-05-28 07:00") };
            var party = new Party { Address="Vägen 13", IsManager=false, LegalId="8705281425", Name="Test Testsson" };
            var dbRepo = new DataBaseRepo();
            var result = (DatabaseHolder)dbRepo.Save(party);
            Assert.Equal(ExecuteCodes.SuccessToExecute, result.ExecuteCodes);
        }

        [Fact]
        public void SelectOne()
        {
            var timePeriod = new TimePeriod { TimePeriodId = 1, StartDate = DateTime.Now, EndDate = DateTime.Parse("2018-05-28 07:00") };
            //var party = new Party { Address="Vägen 12", IsMana,ger=false, LegalId="8705281425", Name="Test Testsson" };
            var dbRepo = new DataBaseRepo();
            var result = dbRepo.GetObject("1", timePeriod);
            Assert.Equal(timePeriod.ToString(), result.ToString());
        }
        [Fact]
        public void FindPartyWithKnownKey()
        {
            var party = new Party { Address = "Vägen 13", IsManager = false, LegalId = "8705281425", Name = "Test Testsson" };
            var dbRepo = new DataBaseRepo();
            //var result = dbRepo.Save(party);
            //var pk = ((DatabaseHolder)result).PrimaryKey;
            Party foundParty = (Party)dbRepo.GetObject("2026", new Party());
            Assert.Equal(2026, foundParty.PartyId);
        }

        [Fact]
        public void GetListFromSearch()
        {
            var party = new Party { PartyId=2026, Address = "Vägen 13", IsManager = false, LegalId = "8705281425", Name = "Test Testsson" };
            var dbRepo = new DataBaseRepo();
            var list = new List<Party>();
            list.Add(party);
            var foundPartyList = dbRepo.GetAll(new Party());
            var list2 = foundPartyList.Cast<Party>().ToList();
            Assert.Equal(party, list2[0]);
        }
    }
}
