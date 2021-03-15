using MACarParkModels.Interfaces;
using MACarParkModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        public ICarPark AddCarPark(ICarPark carPark)
        {
            carParkContext.CarParks.Add((CarPark)carPark);
            carParkContext.SaveChanges();
            return carPark;
        }

        public ICollection<ICarPark> GetCarParks ()
        {
            return carParkContext.CarParks.Include(x=>x.Reservations).ToList<ICarPark>();
        }

        public ICarPark FindCarParkById(int id)
        {
            return carParkContext.CarParks.Include(x => x.Reservations).SingleOrDefault(x=>x.Id == id);
        }

        public void RemoveCarPark(int id)
        {
            var carpark = FindCarParkById(id);
            carParkContext.CarParks.Remove((CarPark)carpark);
            carParkContext.SaveChanges();
        }

        public IReservation AddReservation(IReservation reservation)
        {
            carParkContext.Reservations.Add((Reservation)reservation);
            carParkContext.SaveChanges();
            return reservation;
        }

        public ICollection<IReservation> GetReservationsForCarPark(ICarPark carPark)
        {
            return carParkContext.CarParks.Find(carPark).Reservations as ICollection<IReservation>;
        }

        public IReservation FindReservationById(int id)
        {
            return carParkContext.Reservations.Find(id);
        }

        public void CancelReservation(IReservation reservation)
        {
            carParkContext.Reservations.Remove((Reservation)reservation);
            carParkContext.SaveChanges();
        }

        public ICarPark UpdateCarPark(int carParkId, int availableSpaces)
        {
            var dbCarPark = FindCarParkById(carParkId);
            dbCarPark.AvailableSpaces = availableSpaces;
            carParkContext.SaveChanges();
            return dbCarPark;
        }

        public decimal GetDailyPricePerMonth(int month)
        {
            return carParkContext.DailyPricePerMonths.SingleOrDefault(x => x.Month == month).PricePerDay;
        }
    }
}
