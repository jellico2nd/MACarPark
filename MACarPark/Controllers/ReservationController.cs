using MACarParkModels.Interfaces;
using MACarParkModels.Models;
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
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;
        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [HttpGet]
        [Route("carpark-availability")]
        public ActionResult<IReservation> CarParkAvailibity(int carParkId, DateTime fromDate, DateTime toDate)
        {
            var reservation = new Reservation { CarParkId = carParkId, FromDate = fromDate, ToDate = toDate };
            try
            {
                return Ok(reservationService.GetAvailability(reservation));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet]
        [Route("check-reservation-price")]
        public ActionResult<ReservationWithTotalPriceDTO> CheckReservationPrice(Reservation reservation)
        {
            try
            {
                return Ok(reservationService.GetFullPriceForReservation(reservation));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        [Route("add-reservation")]
        public ActionResult<IReservation> AddReservation(Reservation reservation)
        {
            try
            {
                return CreatedAtAction("AddReservation", reservationService.AddReservation(reservation));
            }
            catch (Exception e)
            {
                //Log Error and Recover 
                //throw;
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("cancel-reservation")]
        public ActionResult CancelReservation(Reservation reservation)
        {
            try
            {
                reservationService.CancelReservation(reservation);
                return Ok();
            }
            catch (Exception e)
            {
                //Log Error and Recover 
                //throw;
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("amend-reservation")]
        public ActionResult<IReservation> AmendReservation(Reservation reservation)
        {
            try
            {
                return Ok(reservationService.UpdateReservation(reservation));
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
