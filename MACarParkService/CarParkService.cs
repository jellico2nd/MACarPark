using MACarParkModels.DataLayer;
using MACarParkModels.Interfaces;
using MACarParkModels.Models;
using MACarParkService.CustomExceptions;
using MACarParkService.DTOs;
using MACarParkService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MACarParkService
{
    public class CarParkService : ICarParkService
    {
        private readonly ICarParkRepository carParkRepository;
        public CarParkService(ICarParkRepository carParkRepository)
        {
            this.carParkRepository = carParkRepository;
        }

        public IReservation AddReservation(IReservation reservation)
        {
            var availability = GetAvailability(reservation);
            try
            {
                CheckForNoFreeSpaces(availability);
            }
            catch
            {
                //Log and recover
                throw;
            }
            var addedReservation = carParkRepository.AddReservation(reservation);
            return addedReservation;
        }

        public void CancelReservation(IReservation reservation)
        {
            throw new NotImplementedException();
        }

        public ICarPark FindCarParkById(int id)
        {
            return carParkRepository.FindCarParkById(id);
        }

        public IReservation FindReservationById(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<AvailabilityDTO> GetAvailability(IReservation reservation)
        {
            var availableSpacesByDate = new List<AvailabilityDTO>();
            var carPark = carParkRepository.FindCarParkById(reservation.CarParkId);
            for (int i = 0; i <= (reservation.ToDate - reservation.FromDate).Days; i++)
            {
                var reservationDay = reservation.FromDate.AddDays(i);
                var takenReservations = carPark.Reservations.Where(x => x.FromDate <= reservationDay && x.ToDate >= reservationDay);
                availableSpacesByDate.Add(new AvailabilityDTO() 
                { 
                    ReservationDate = reservationDay, 
                    SpacesAvailability = FormatAvailabilityString(carPark, takenReservations.Count()), 
                    FreeSpaces = carPark.AvailableSpaces - takenReservations.Count() });
            }
            return availableSpacesByDate;
        }

        public ICollection<ICarPark> GetCarParks()
        {
            return carParkRepository.GetCarParks();
        }

        public ICarPark UpdateCarpark(int id, int availableSpaces)
        {
            var carPark = FindCarParkById(id);
            if (carPark.AvailableSpaces != availableSpaces)
            {
                return carParkRepository.UpdateCarPark(id, availableSpaces);
            }
            return new CarPark();
        }

        public ICarPark AddCarPark(ICarPark carPark)
        {
            carParkRepository.AddCarPark(carPark);
            var result = carParkRepository.FindCarParkById(carPark.Id);
            return result;
        }

        private string FormatAvailabilityString(ICarPark carPark, int takenReservations)
        {
            if(takenReservations == 0)
            {
                return "all free spaces";
            }
            if(carPark.AvailableSpaces - takenReservations == 0)
            {
                return "no free spaces";
            }
            return $"{carPark.AvailableSpaces - takenReservations} free spaces";
        }

        private static void CheckForNoFreeSpaces(ICollection<AvailabilityDTO> availability)
        {
            if (availability.Any(x => x.FreeSpaces == 0))
            {
                string message = string.Empty;
                foreach (var item in availability.Where(x => x.FreeSpaces == 0))
                {
                    message += $"{item.ReservationDate.ToShortDateString()} - {item.SpacesAvailability}";
                }
                throw new NoFreeSpacesException(message);
            }
        }
    }
}
