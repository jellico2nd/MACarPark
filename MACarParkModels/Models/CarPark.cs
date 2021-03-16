using MACarParkModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MACarParkModels.Models
{
    public class CarPark : ICarPark
    {
        public CarPark()
        {
        }
        public CarPark(int id, int availableSpaces)
        {
            Id = id;
            AvailableSpaces = availableSpaces;
        }
        public int Id { get; set; }
        public int AvailableSpaces { get; set; } = 0;
    }
}
