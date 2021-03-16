using MACarParkModels.Interfaces;
using MACarParkService.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MACarParkService.Interfaces
{
    public interface IReservationService
    {
        IReservation FindReservationById(int id);
        IReservation AddReservation(IReservation reservation);
        IReservation UpdateReservation(IReservation reservation);
        void CancelReservation(IReservation reservation);
        ReservationWithTotalPriceDTO GetFullPriceForReservation(IReservation reservation);
        ICollection<AvailabilityDTO> GetAvailability(IReservation reservation);
    }
}
