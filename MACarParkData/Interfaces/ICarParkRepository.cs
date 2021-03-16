using MACarParkData.Entities;
using MACarParkModels.Interfaces;
using MACarParkModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MACarParkData.Interfaces
{
    public interface ICarParkRepository
    {
        ICollection<CarParkEntity> GetCarParks();
        CarParkEntity AddCarPark(CarParkEntity carPark);
        CarParkEntity FindCarParkById(int id);
        CarParkEntity UpdateCarPark(int id, int availableSpaces);
        void RemoveCarPark(int id);
    }
}
