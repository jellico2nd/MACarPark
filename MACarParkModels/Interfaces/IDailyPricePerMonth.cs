using System;
using System.Collections.Generic;
using System.Text;

namespace MACarParkModels.Interfaces
{
    public interface IDailyPricePerMonth
    {
        int Month { get; set; }
        decimal PricePerDay { get; set; }
    }
}
