using MACarParkModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MACarParkModels.DataLayer
{
    public class CarParkContext : DbContext
    {
        public CarParkContext(DbContextOptions<CarParkContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyPricePerMonth>().HasData(
                new DailyPricePerMonth { Month = 1, PricePerDay = 10.95m },
                new DailyPricePerMonth { Month = 2, PricePerDay = 4.95m },
                new DailyPricePerMonth { Month = 3, PricePerDay = 5.95m },
                new DailyPricePerMonth { Month = 4, PricePerDay = 5.95m },
                new DailyPricePerMonth { Month = 5, PricePerDay = 7.95m },
                new DailyPricePerMonth { Month = 6, PricePerDay = 8.95m },
                new DailyPricePerMonth { Month = 7, PricePerDay = 8.95m },
                new DailyPricePerMonth { Month = 8, PricePerDay = 8.95m },
                new DailyPricePerMonth { Month = 9, PricePerDay = 7.95m },
                new DailyPricePerMonth { Month = 10, PricePerDay = 5.95m },
                new DailyPricePerMonth { Month = 11, PricePerDay = 5.95m },
                new DailyPricePerMonth { Month = 12, PricePerDay = 10.95m }
                );
            modelBuilder.Entity<CarPark>().HasData(
                new CarPark { Id = 1, AvailableSpaces = 10}
                );
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation { Id = 1, CarParkId = 1, FromDate = new DateTime(2021,3,15), ToDate = new DateTime(2021, 3, 20) }
                );
        }
        public DbSet<CarPark> CarParks { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<DailyPricePerMonth> DailyPricePerMonths {get;set;}
    }
}
