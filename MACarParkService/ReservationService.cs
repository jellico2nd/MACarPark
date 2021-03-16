using MACarParkData.Interfaces;
using MACarParkModels.Interfaces;
using MACarParkModels.Models;
using MACarParkService.CustomExceptions;
using MACarParkService.DTOs;
using MACarParkService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MACarParkService
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IParkingPricesService parkingPricesService;
        private readonly ICarParkRepository carParkRepository;
        public ReservationService(IReservationRepository reservationRepository, IParkingPricesService parkingPricesService, ICarParkRepository carParkRepository)
        {
            this.reservationRepository = reservationRepository;
            this.parkingPricesService = parkingPricesService;
            this.carParkRepository = carParkRepository;
        }

        public IReservation AddReservation(IReservation reservation)
        {
            var availability = GetAvailability(reservation);
            CheckForNoFreeSpaces(availability);
            var addedReservation = reservationRepository.FindReservationById(reservation.Id);
            addedReservation.Id = reservation.Id;
            addedReservation.CarParkId = reservation.CarParkId;
            addedReservation.FromDate = reservation.FromDate;
            addedReservation.ToDate = reservation.ToDate;
            reservationRepository.AddReservation(addedReservation);
            return new Reservation(addedReservation.Id, addedReservation.CarParkId, addedReservation.FromDate, addedReservation.ToDate);
        }

        public void CancelReservation(IReservation reservation)
        {
            var reservationToCancel = reservationRepository.FindReservationById(reservation.Id);
            reservationRepository.CancelReservation(reservationToCancel);
        }

        public IReservation FindReservationById(int id)
        {
            var reservation = reservationRepository.FindReservationById(id);
            return new Reservation(reservation.Id, reservation.CarParkId, reservation.FromDate, reservation.ToDate);
        }

        public ReservationWithTotalPriceDTO GetFullPriceForReservation(IReservation reservation)
        {
            var availability = GetAvailability(reservation);
            return new ReservationWithTotalPriceDTO { CarParkAvailability = availability };
        }

        public IReservation UpdateReservation(IReservation reservation)
        {
            GetAvailability(reservation);
            var currentReservation = reservationRepository.FindReservationById(reservation.Id);
            currentReservation.CarParkId = reservation.CarParkId;
            currentReservation.FromDate = reservation.FromDate;
            currentReservation.ToDate = reservation.ToDate;
            reservationRepository.UpdateReservation(currentReservation);
            return new Reservation(currentReservation.Id, currentReservation.CarParkId, currentReservation.FromDate, currentReservation.ToDate);
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
                    SpacesAvailability = FormatAvailabilityString(carPark.AvailableSpaces, takenReservations.Count()),
                    FreeSpaces = carPark.AvailableSpaces - takenReservations.Count(),
                    Price = parkingPricesService.GetParkingPrice(reservationDay.Month)
                });
            }
            return availableSpacesByDate;
        }

        private static void CheckForNoFreeSpaces(ICollection<AvailabilityDTO> availability)
        {
            if (availability.Any(x => x.FreeSpaces == 0))
            {
                var message = new StringBuilder();
                foreach (var item in availability.Where(x => x.FreeSpaces == 0))
                {
                    message.AppendLine($"{item.ReservationDate.ToShortDateString()} - {item.SpacesAvailability}");
                }
                throw new NoFreeSpacesException(message.ToString());
            }
        }
        private string FormatAvailabilityString(int availableSpaces, int takenReservations)
        {
            if (takenReservations == 0)
            {
                return "all free spaces";
            }
            if (availableSpaces - takenReservations == 0)
            {
                return "no free spaces";
            }
            return $"{availableSpaces - takenReservations} free spaces";
        }
    }
}
