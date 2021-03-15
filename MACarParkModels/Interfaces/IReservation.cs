using System;
using System.Collections.Generic;
using System.Text;

namespace MACarParkModels.Interfaces
{
    public interface IReservation
    {
        int Id { get; set; }
        int CarParkId { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
    }
}
