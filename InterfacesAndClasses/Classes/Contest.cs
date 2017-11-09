using System;
using System.Collections.Generic;
using System.Text;

namespace AccountabilityLib.Classes
{
    public class Contest
    {
        public int ContestId { get; set; }
        public int ManagerId { get; set; }
        public int TimePeriodId { get; set; }
        public int ContestTypeId { get; set; }
        public int Winner { get; set; }
        //public Party Competitor { get; set; }
        //public Party Manager { get; set; }
        //public TimePeriod TimePeriod { get; set; }
        //public ContestType ContestType { get; set; }

        public Contest CreateContest()
        {
            return new Contest();
        }
    }
}
