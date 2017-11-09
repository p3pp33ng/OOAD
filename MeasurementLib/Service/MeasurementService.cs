using BowlingLib;
using DatabaseRepoLib.Classes;
using MeasurementLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static DatabaseRepoLib.Classes.DataBaseRepo;

namespace BowlingLib.Service
{
    public class MeasurementService
    {
        public Unit WhatUnitDoYouNeedBro(string unitName)
        {

            Unit unit = new Unit();
            DataBaseRepo database = new DataBaseRepo();
            foreach (Unit item in database.GetAll(new Unit()).Cast<Unit>().ToList())
            {
                if (item.Name.ToLower() == unitName.ToLower())
                {
                    unit = item;
                }
            }
            return unit;
        }

        public Quantity CreateANewQuantity(int amount, int unitId)
        {
            DataBaseRepo database = new DataBaseRepo();
            var result = (DatabaseHolder)database.Save(new Quantity());
            var quantity = (Quantity)database.GetObject(result.PrimaryKey.ToString(), new Quantity());
            return quantity;
        }
    }
}
