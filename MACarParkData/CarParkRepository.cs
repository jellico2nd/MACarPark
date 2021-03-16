using MACarParkModels.Interfaces;
using MACarParkModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MACarParkData.Interfaces;
using MACarParkData;
using MACarParkData.Entities;

namespace MACarParkModels.DataLayer
{
    //All this should be in seperate project but I didn't had the time to clean it up
    public class CarParkRepository: ICarParkRepository
    {
        private readonly CarParkContext carParkContext;
        public CarParkRepository(CarParkContext carParkContext)
        {
            carParkContext.Database.EnsureCreated();
            this.carParkContext = carParkContext;
        }

        public CarParkEntity AddCarPark(CarParkEntity carPark)
        {
            carParkContext.CarParks.Add(carPark);
            carParkContext.SaveChanges();
            return carPark;
        }

        public ICollection<CarParkEntity> GetCarParks ()
        {
            return carParkContext.CarParks.Include(x=>x.Reservations).ToList();
        }

        public CarParkEntity FindCarParkById(int id)
        {
            return carParkContext.CarParks.Include(x => x.Reservations).SingleOrDefault(x=>x.Id == id);
        }

        public void RemoveCarPark(int id)
        {
            var carpark = FindCarParkById(id);
            carParkContext.CarParks.Remove(carpark);
            carParkContext.SaveChanges();
        }

        public CarParkEntity UpdateCarPark(int carParkId, int availableSpaces)
        {
            var dbCarPark = FindCarParkById(carParkId);
            dbCarPark.AvailableSpaces = availableSpaces;
            carParkContext.SaveChanges();
            return dbCarPark;
        }
    }
}
