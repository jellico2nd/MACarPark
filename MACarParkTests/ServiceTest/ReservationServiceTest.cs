using FakeItEasy;
using MACarParkData.Entities;
using MACarParkData.Interfaces;
using MACarParkModels.Models;
using MACarParkService;
using MACarParkService.CustomExceptions;
using MACarParkService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MACarParkTests.ServiceTest
{
    public class ReservationServiceTest
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IParkingPricesService parkingPricesService;
        private readonly ICarParkRepository carParkRepository;

        private List<ReservationEntity> reservations = new List<ReservationEntity>
        {
            new ReservationEntity{ Id = 1, CarParkId = 1, FromDate = new DateTime(2021, 3, 15), ToDate = new DateTime(2021, 3, 20)}
        };
        private List<CarParkEntity> carParkList = new List<CarParkEntity> 
        { 
            new CarParkEntity { Id = 1, AvailableSpaces = 2 }, 
            new CarParkEntity { Id = 2, AvailableSpaces = 5 } 
        };

        public ReservationServiceTest()
        {
            reservationRepository = A.Fake<IReservationRepository>();
            parkingPricesService = A.Fake<IParkingPricesService>();
            carParkRepository = A.Fake<ICarParkRepository>();
        }

        [Fact]
        public void CheckAvailability_ForReservation_ReturnsAllDetails()
        {
            var dailyPrice = 4.99m;
            var carPark = carParkList[0];
            carPark.Reservations = reservations;
            var newReservation = new Reservation { Id = 2, CarParkId = carPark.Id, FromDate = new DateTime(2021, 3, 17), ToDate = new DateTime(2021, 3, 22) };

            A.CallTo(carParkRepository).Where(call => call.Method.Name == "FindCarParkById").WithReturnType<CarParkEntity>().Returns(carPark);
            A.CallTo(parkingPricesService).Where(call => call.Method.Name == "GetParkingPrice").WithReturnType<decimal>().Returns(dailyPrice);

            var SUT = new ReservationService(reservationRepository, parkingPricesService, carParkRepository);
            var result = SUT.GetAvailability(newReservation);

            Assert.Equal(4, result.Where(x => x.FreeSpaces == 1).Count());
            Assert.True(result.All(x => x.Price != default));
        }


        [Fact]
        public void AddNewReservation_FullCarPark_ReturnsExceptionNoFreeSpaces()
        {
            var failingFromDate = new DateTime(2021, 3, 17);
            var failingToDate = new DateTime(2021, 3, 18);
            var dailyPrice = 4.99m;
            var carPark = carParkList[0];
            carPark.Reservations = reservations;
            carPark.Reservations.Add(new ReservationEntity { Id = 2, CarParkId = carPark.Id, FromDate = new DateTime(2021, 3, 17), ToDate = new DateTime(2021, 3, 22) });
            var newReservation = new Reservation { Id = 2, CarParkId = carPark.Id, FromDate = failingFromDate, ToDate = failingToDate };
            var expectedMessage = new StringBuilder();
            expectedMessage.AppendLine($"{failingFromDate.ToShortDateString()} - no free spaces");
            expectedMessage.AppendLine($"{failingToDate.ToShortDateString()} - no free spaces");

            A.CallTo(carParkRepository).Where(call => call.Method.Name == "FindCarParkById").WithReturnType<CarParkEntity>().Returns(carPark);
            A.CallTo(parkingPricesService).Where(call => call.Method.Name == "GetParkingPrice").WithReturnType<decimal>().Returns(dailyPrice);

            var SUT = new ReservationService(reservationRepository, parkingPricesService, carParkRepository);
            var result = Assert.Throws<NoFreeSpacesException>(() => SUT.AddReservation(newReservation));

            Assert.Equal(expectedMessage.ToString(), result.Message);
        }
    }
}
