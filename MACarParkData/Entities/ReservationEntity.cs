using MACarParkData.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MACarParkData.Entities
{
    public class ReservationEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int CarParkId { get; set; }
        [ForeignKey("CarParkId")]
        public CarParkEntity CarPark { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
