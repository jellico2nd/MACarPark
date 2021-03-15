using MACarParkModels.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MACarParkModels.Interfaces
{
    public interface ICarParkRepository
    {
        ICollection<ICarPark> GetCarParks();
        ICarPark AddCarPark(ICarPark carPark);
        ICarPark FindCarParkById(int id);
        ICarPark UpdateCarPark(int id, int availableSpaces);
        void RemoveCarPark(int id);
        IReservation AddReservation(IReservation reservation);
        ICollection<IReservation> GetReservationsForCarPark(ICarPark carPark);
        IReservation FindReservationById(int id);
        void CancelReservation(IReservation reservation);
    }
}
