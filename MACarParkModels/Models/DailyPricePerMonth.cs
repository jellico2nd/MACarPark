using MACarParkModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MACarParkModels.Models
{
    public class DailyPricePerMonth : IDailyPricePerMonth
    {
        public int Month { get; set; }
        public decimal PricePerDat { get; set; }
    }
}
