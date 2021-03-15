using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MACarParkService.DTOs
{
    public class ReservationWithTotalPriceDTO
    {
        public ICollection<AvailabilityDTO> CarParkAvailability { get; set; }
        public decimal TotalPriceForCarPark => CarParkAvailability.Sum(x => x.Price);
    }
}
