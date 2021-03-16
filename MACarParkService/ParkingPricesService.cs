using MACarParkData.Interfaces;
using MACarParkService.Interfaces;

namespace MACarParkService
{
    public class ParkingPricesService : IParkingPricesService
    {
        private readonly IParkingPriceRepository parkingPriceRepository;
        public ParkingPricesService(IParkingPriceRepository parkingPriceRepository)
        {
            this.parkingPriceRepository = parkingPriceRepository;
        }
        public decimal GetParkingPrice(int month)
        {
            return parkingPriceRepository.GetDailyPricePerMonth(month);
        }
    }
}
