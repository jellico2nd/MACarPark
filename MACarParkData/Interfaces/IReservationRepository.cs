using MACarParkData.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MACarParkData.Interfaces
{
    public interface IReservationRepository
    {
        ReservationEntity AddReservation(ReservationEntity reservation);
        ReservationEntity UpdateReservation(ReservationEntity reservation);
        ICollection<ReservationEntity> GetReservationsForCarPark(CarParkEntity carPark);
        ReservationEntity FindReservationById(int id);
        void CancelReservation(ReservationEntity reservation);
    }
}
