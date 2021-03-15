using MACarParkModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MACarParkModels.DataLayer
{
    public class CarParkContext : DbContext
    {
        public CarParkContext(DbContextOptions<CarParkContext> options): base(options)
        {
        }

        public DbSet<CarPark> CarParks { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
