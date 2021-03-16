using MACarParkModels.Interfaces;
using MACarParkModels.Models;
using MACarParkService.DTOs;
using System.Collections.Generic;

namespace MACarParkService.Interfaces
{
    public interface ICarParkService
    {
        ICollection<ICarPark> GetCarParks();
        ICarPark AddCarPark(ICarPark carPark);
        ICarPark FindCarParkById(int id);
        ICarPark UpdateCarpark(int id, int availableSpaces);
    }
}
