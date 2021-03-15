using MACarParkModels.Interfaces;
using MACarParkModels.Models;
using MACarParkService;
using MACarParkService.DTOs;
using MACarParkService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MACarPark.Controllers
{
    [Route("api/carpark")]
    [ApiController]
    public class CarParkController : ControllerBase
    {
        private readonly ICarParkService carParkService;

        public CarParkController(ICarParkService carParkService)
        {
            this.carParkService = carParkService;
        }

        [HttpGet]
        [Route("get-carparks")]
        public ActionResult<ICollection<ICarPark>> GetCarParks()
        {
            return Ok(carParkService.GetCarParks());
        }

        [HttpGet]
        [Route("get-carpark-availability")]
        public ActionResult<IReservation> GetCarParkAvailibity(int carParkId, DateTime fromDate, DateTime toDate)
        {
            var reservation = new Reservation { CarParkId = carParkId, FromDate = fromDate, ToDate = toDate };
            try
            {
                return Ok(carParkService.GetAvailability(reservation));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet]
        [Route("get-reservation-price")]
        public ActionResult<ReservationWithTotalPriceDTO> GetReservationPrice(int carParkId, DateTime fromDate, DateTime toDate)
        {
            var reservation = new Reservation { CarParkId = carParkId, FromDate = fromDate, ToDate = toDate };
            try
            {
                return Ok(carParkService.GetFullPriceForReservation(reservation));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        [Route("add-carpark")]
        public ActionResult<ICarPark> AddCarpark(CarPark carPark)
        {
            try
            {
                return Ok(carParkService.AddCarPark(carPark));
            }
            catch (Exception)
            {
                //   Log Error and Recover
                //throw;
                return NoContent();
            }

        }

        [HttpPost]
        [Route("change-spaces")]
        public ActionResult ChangeCarParkAvailableSpaces(int id, int availableSpaces)
        {
            try
            {
                return Ok(carParkService.UpdateCarpark(id, availableSpaces));
            }
            catch (Exception)
            {
                //Log and recover
                //throw;
                return NoContent();
            }
            

        }

        [HttpPost]
        [Route("add-reservation")]
        public ActionResult<IReservation> AddReservation(Reservation reservation)
        {
            try
            {
                return CreatedAtAction("AddReservation", carParkService.AddReservation(reservation));
            }
            catch (Exception e)
            {
                //Log Error and Recover 
                //throw;
                return BadRequest(e.Message);
            }
        }
    }
}
