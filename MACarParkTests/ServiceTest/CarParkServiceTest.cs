using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FakeItEasy;
using MACarParkData.Interfaces;
using MACarParkService.Interfaces;
using MACarParkService;
using MACarParkData.Entities;
using MACarParkModels.Models;

namespace MACarParkTests.ServiceTest
{
    public class CarParkServiceTest
    {
        private readonly ICarParkRepository carParkRepository;
        private readonly List<CarParkEntity> carParkList = new List<CarParkEntity> { new CarParkEntity { Id = 1, AvailableSpaces = 2 }, new CarParkEntity { Id = 2, AvailableSpaces = 5 } };

        public CarParkServiceTest()
        {
            carParkRepository = A.Fake<ICarParkRepository>();         
        }
        
        [Fact]
        public void GetCarparks_ListOfCarParks_Returned()
        {
            A.CallTo(() => carParkRepository.GetCarParks()).Returns(carParkList);
            var SUT = new CarParkService(carParkRepository);

            var result = SUT.GetCarParks();

            Assert.Equal(carParkList.Count, result.Count);
        }

        [Fact]
        public void FindById_CarPark_Returned()
        {
            int id = 1;
            A.CallTo(carParkRepository).Where(call => call.Method.Name == "FindCarParkById").WithReturnType<CarParkEntity>().Returns(carParkList[0]);
            var SUT = new CarParkService(carParkRepository);

            var result = SUT.FindCarParkById(id);

            Assert.NotNull(result);
        }

        [Fact]
        public void AddNew_CarPark_CarParkReturned()
        {
            int id = 1;
            var carParkEntity = new CarParkEntity { Id = id, AvailableSpaces = 2, Reservations = new List<ReservationEntity>() };
            var carPark = new CarPark { Id = id, AvailableSpaces = 2 };
            A.CallTo(carParkRepository).Where(call => call.Method.Name == "FindCarParkById").WithReturnType<CarParkEntity>().Returns(new CarParkEntity());
            A.CallTo(carParkRepository).Where(call => call.Method.Name == "AddCarPark").WithReturnType<CarParkEntity>().Returns(carParkEntity);

            var SUT = new CarParkService(carParkRepository);

            var result = SUT.AddCarPark(carPark);

            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateCarPark_AvailableSpacesEqual_CarParkNotUpdated()
        {
            int id = 1;
            int availableSpaces = 2;
            var carParkEntity = new CarParkEntity { Id = id, AvailableSpaces = availableSpaces, Reservations = new List<ReservationEntity>() };
            A.CallTo(carParkRepository).Where(call => call.Method.Name == "FindCarParkById")
                  .WithReturnType<CarParkEntity>()
                  .Returns(carParkEntity);
            var SUT = new CarParkService(carParkRepository);

            SUT.UpdateCarpark(id, availableSpaces);

            A.CallTo(carParkRepository).Where(call => call.Method.Name == "UpdateCarPark").MustNotHaveHappened();
        }

        [Fact]
        public void UpdateCarPark_AvailableSpaces_CarParkUpdated()
        {
            int id = 1;
            int availableSpaces = 2;
            int newAvailableSpaces = 3;
            var carParkEntity = new CarParkEntity { Id = id, AvailableSpaces = availableSpaces, Reservations = new List<ReservationEntity>() };
            A.CallTo(carParkRepository).Where(call => call.Method.Name == "FindCarParkById")
                  .WithReturnType<CarParkEntity>()
                  .Returns(carParkEntity);
            var SUT = new CarParkService(carParkRepository);

            SUT.UpdateCarpark(id, newAvailableSpaces);

            A.CallTo(carParkRepository).Where(call => call.Method.Name == "UpdateCarPark").MustHaveHappened();
        }
    }
}
