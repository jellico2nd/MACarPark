using System;
using System.Collections.Generic;
using System.Text;

namespace MACarParkData.Interfaces
{
    public interface IParkingPriceRepository
    {
        decimal GetDailyPricePerMonth(int month);
    }
}
