using MACarParkModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MACarParkModels.Models
{
    public class CarPark : ICarPark
    {
        [Key]
        public int Id { get; set; }
        public int AvailableSpaces { get; set; } = 0;
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
