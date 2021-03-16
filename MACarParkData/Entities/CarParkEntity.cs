using MACarParkData.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MACarParkData.Entities
{
    public class CarParkEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int AvailableSpaces { get; set; } = 0;
        public ICollection<ReservationEntity> Reservations { get; set; } = new List<ReservationEntity>();
    }
}
