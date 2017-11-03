using System;
using System.Collections.Generic;
using System.Text;
using MeasurementLib;

namespace BowlingLib
{
    public class Score
    {
        public int ScoreId { get; set; }
        public int UnitId { get; set; }
        public int QuantityId { get; set; }
        public int LaneId { get; set; }
        public Unit Unit { get; set; }
        public Quantity Quantity { get; set; }
        public Lane Lane { get; set; }
    }
}
