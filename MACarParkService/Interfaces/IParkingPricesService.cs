using System;
using System.Collections.Generic;
using System.Text;

namespace MACarParkService.Interfaces
{
    public interface IParkingPricesService
    {
        decimal GetParkingPrice(int month);
    }
}
