using MACarParkData.Entities;
using MACarParkData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MACarParkData
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly CarParkContext carParkContext;
        public ReservationRepository(CarParkContext carParkContext)
        {
            this.carParkContext = carParkContext;
        }
        public ReservationEntity AddReservation(ReservationEntity reservation)
        {
            carParkContext.Reservations.Add(reservation);
            carParkContext.SaveChanges();
            return reservation;
        }

        public void CancelReservation(ReservationEntity reservation)
        {
            carParkContext.Reservations.Remove(reservation);
            carParkContext.SaveChanges();
        }

        public ReservationEntity FindReservationById(int id)
        {
            return carParkContext.Reservations.SingleOrDefault(x => x.Id == id) ?? new ReservationEntity();
        }

        public ICollection<ReservationEntity> GetReservationsForCarPark(CarParkEntity carPark)
        {
            return carParkContext.CarParks.Find(carPark).Reservations;
        }

        public ReservationEntity UpdateReservation(ReservationEntity reservation)
        {
            var currentReservation = carParkContext.Reservations.SingleOrDefault(x => x.Id == reservation.Id);
            if(currentReservation == null)
            {
                currentReservation = AddReservation(reservation);
            }
            currentReservation.FromDate = reservation.FromDate;
            currentReservation.ToDate = reservation.ToDate;
            carParkContext.SaveChanges();
            return currentReservation;
        }
    }
}
