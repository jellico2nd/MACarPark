using MACarParkModels.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MACarParkModels.Models
{
    public class Reservation : IReservation
    {
        public Reservation()
        {
        }
        public Reservation(int id, int carParkId, DateTime fromDate, DateTime toDate)
        {
            Id = id;
            CarParkId = carParkId;
            FromDate = fromDate;
            ToDate = toDate;
        }
        public int Id { get; set; }
        public int CarParkId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
