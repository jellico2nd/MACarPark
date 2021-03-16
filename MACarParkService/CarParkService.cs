using MACarParkData.Interfaces;
using MACarParkModels.Interfaces;
using MACarParkModels.Models;
using MACarParkService.Interfaces;
using System.Collections.Generic;

namespace MACarParkService
{
    public class CarParkService : ICarParkService
    {
        private readonly ICarParkRepository carParkRepository;
        private readonly IParkingPricesService parkingPricesService;

        public CarParkService(ICarParkRepository carParkRepository, IParkingPricesService parkingPricesService)
        {
            this.carParkRepository = carParkRepository;
            this.parkingPricesService = parkingPricesService;
        }

        public ICarPark FindCarParkById(int id)
        {
            var carParkEntity = carParkRepository.FindCarParkById(id);
            return new CarPark(carParkEntity.Id, carParkEntity.AvailableSpaces);
        }

        public ICollection<ICarPark> GetCarParks()
        {
            var carParks = carParkRepository.GetCarParks();
            var listOfCarparks = new List<ICarPark>();
            foreach (var item in carParks)
            {
                listOfCarparks.Add(new CarPark(item.Id, item.AvailableSpaces));
            }
            return listOfCarparks;
        }

        public ICarPark UpdateCarpark(int id, int availableSpaces)
        {
            var carPark = FindCarParkById(id);
            if (carPark.AvailableSpaces != availableSpaces)
            {
                var updatedCapPark = carParkRepository.UpdateCarPark(id, availableSpaces);
                return new CarPark(updatedCapPark.Id, updatedCapPark.AvailableSpaces);
            }
            return new CarPark();
        }

        public ICarPark AddCarPark(ICarPark carPark)
        {
            var result = carParkRepository.FindCarParkById(carPark.Id);
            result.Id = carPark.Id;
            result.AvailableSpaces = carPark.AvailableSpaces;
            carParkRepository.AddCarPark(result);
            return new CarPark(result.Id, result.AvailableSpaces);
        }

    }
}
