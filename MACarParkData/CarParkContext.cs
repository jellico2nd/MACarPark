﻿using MACarParkData.Entities;
using MACarParkModels.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MACarParkData
{
    public class CarParkContext : DbContext
    {
        public CarParkContext(DbContextOptions<CarParkContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyPricePerMonthEntity>().HasData(
                new DailyPricePerMonthEntity { Month = 1, PricePerDay = 10.95m },
                new DailyPricePerMonthEntity { Month = 2, PricePerDay = 4.95m },
                new DailyPricePerMonthEntity { Month = 3, PricePerDay = 5.95m },
                new DailyPricePerMonthEntity { Month = 4, PricePerDay = 5.95m },
                new DailyPricePerMonthEntity { Month = 5, PricePerDay = 7.95m },
                new DailyPricePerMonthEntity { Month = 6, PricePerDay = 8.95m },
                new DailyPricePerMonthEntity { Month = 7, PricePerDay = 8.95m },
                new DailyPricePerMonthEntity { Month = 8, PricePerDay = 8.95m },
                new DailyPricePerMonthEntity { Month = 9, PricePerDay = 7.95m },
                new DailyPricePerMonthEntity { Month = 10, PricePerDay = 5.95m },
                new DailyPricePerMonthEntity { Month = 11, PricePerDay = 5.95m },
                new DailyPricePerMonthEntity { Month = 12, PricePerDay = 10.95m }
                );
            modelBuilder.Entity<CarParkEntity>().HasData(
                new CarParkEntity { Id = 1, AvailableSpaces = 10}
                );
            modelBuilder.Entity<ReservationEntity>().HasData(
                new ReservationEntity { Id = 1, CarParkId = 1, FromDate = new DateTime(2021,3,15), ToDate = new DateTime(2021, 3, 20) }
                );
        }
        public DbSet<CarParkEntity> CarParks { get; set; }
        public DbSet<ReservationEntity> Reservations { get; set; }
        public DbSet<DailyPricePerMonthEntity> DailyPricePerMonths {get;set;}
    }
}
