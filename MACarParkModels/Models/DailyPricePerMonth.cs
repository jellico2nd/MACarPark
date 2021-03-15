using MACarParkModels.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MACarParkModels.Models
{
    public class DailyPricePerMonth : IDailyPricePerMonth
    {
        [Key]
        public int Month { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
