using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MACarParkModels.DataLayer;
using MACarParkModels.Interfaces;
using MACarParkService.Interfaces;
using MACarParkService;
using MACarParkModels.Models;
using MACarParkData;
using MACarParkData.Interfaces;

namespace MACarPark
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CarParkContext>(opt => opt.UseInMemoryDatabase("CarParks"));

            services.AddScoped<ICarParkRepository, CarParkRepository>();
            services.AddScoped<IParkingPriceRepository, ParkingPriceRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<ICarParkService, CarParkService>();
            services.AddScoped<IParkingPricesService, ParkingPricesService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<ICarPark, CarPark>();
            services.AddScoped<IReservation, Reservation>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
