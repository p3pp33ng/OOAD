using BowlingLib;
using DatabaseRepoLib.Classes;
using MeasurementLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingLib.Service
{
    public class MeasurementService
    {
        public Unit WhatUnitDoYouNeedBro(string unitName)
        {
            DataBaseRepo database = new DataBaseRepo();
            Unit unit = new Unit();

            foreach (Unit item in database.GetAll(new Unit()))
            {
                if (item.Name.ToLower() == unitName.ToLower())
                {
                    unit = item;
                }
            }
            return unit;
        }
    }
}
