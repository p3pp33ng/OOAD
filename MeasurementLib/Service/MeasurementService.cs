using BowlingLib;
using DatabaseRepoLib.Classes;
using MeasurementLib;
using System;
using System.Collections.Generic;
using System.Text;
using static DatabaseRepoLib.Classes.DataBaseRepo;

namespace BowlingLib.Service
{
    public class MeasurementService
    {
        private DataBaseRepo database;
        public Unit WhatUnitDoYouNeedBro(string unitName)
        {
            
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

        public Quantity CreateANewQuantity(int amount, int unitId)
        {
            var result = (DatabaseHolder)database.Save(new Quantity());
            var quantity = (Quantity)database.GetObject(result.PrimaryKey.ToString(), new Quantity());
            return quantity;
        }
    }
}
