using MACarParkModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MACarParkModels.Interfaces
{
    public interface ICarPark
    {
        int Id { get; set; }
        int AvailableSpaces { get; set; }
        ICollection<Reservation> Reservations { get; set; }
    }
}
