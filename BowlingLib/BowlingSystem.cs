using AccountabilityLib;
using AccountabilityLib.Classes;
using DatabaseRepoLib.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using static DatabaseRepoLib.Classes.DataBaseRepo;

namespace BowlingLib
{
    public class BowlingSystem
    {
        public void GetWinnerOfContest()
        {

        }

        public Party CreateANewPlayer(string legalId, string name, string address, bool isManager = false)
        {
            var member = new Party { Address = address, IsManager = isManager, LegalId = legalId, Name = name };
            var dataBase = new DataBaseRepo();
            var databaseHolder = (DatabaseHolder)dataBase.Save(member);
            member.PartyId = databaseHolder.PrimaryKey;
            return member;
        }

        public Party CreateANewManager(string legalId, string name, string address, bool isManager = true)
        {
            var member = new Party { Address = address, IsManager = isManager, LegalId = legalId, Name = name };
            var dataBase = new DataBaseRepo();
            var databaseHolder = (DatabaseHolder)dataBase.Save(member);
            member.PartyId = databaseHolder.PrimaryKey;
            return member;
        }

        public void CreateANewContest(int[] competitors, int managerId, int timePeriodId, int contestTypeId)
        {
            var database = new DataBaseRepo();

            for (int i = 0; i < competitors.Length; i++)
            {
                var contest = new Contest
                {
                    CompetitorId = competitors[i],
                    ContestTypeId = contestTypeId,
                    ManagerId = managerId,
                    TimePeriodId = timePeriodId
                };
                database.Save(contest);
            }
        }

        public void SeeMatches()
        {

        }
    }
}
