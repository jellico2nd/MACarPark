using MACarParkData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MACarParkData
{
    public class ParkingPriceRepository : IParkingPriceRepository
    {
        private readonly CarParkContext carParkContext;
        public ParkingPriceRepository(CarParkContext carParkContext)
        {
            this.carParkContext = carParkContext;
        }
        public decimal GetDailyPricePerMonth(int month)
        {
            return carParkContext.DailyPricePerMonths.SingleOrDefault(x => x.Month == month).PricePerDay;
        }
    }
}
