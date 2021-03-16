using MACarParkData.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MACarParkData.Entities
{
    public class DailyPricePerMonthEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public int Month { get; set; }
        public decimal PricePerDay { get; set; }

    }
}
