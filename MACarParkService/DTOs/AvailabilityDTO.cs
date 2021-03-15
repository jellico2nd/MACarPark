using System;
using System.Collections.Generic;
using System.Text;

namespace MACarParkService.DTOs
{
    public class AvailabilityDTO
    {
        public DateTime ReservationDate { get; set; }
        public string SpacesAvailability { get; set; }
        public int FreeSpaces { get; set; }
    }
}
