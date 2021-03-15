using MACarParkModels.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MACarParkModels.Models
{
    public class Reservation : IReservation
    {
        [Key]
        public int Id { get; set; }
        public int CarParkId { get; set; }
        [JsonIgnore]
        [ForeignKey("CarParkId")]
        public CarPark CarPark { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
