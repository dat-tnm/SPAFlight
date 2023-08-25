using BookingFlightApp.Data;
using BookingFlightApp.Domain.Entities;
using BookingFlightApp.DTOs;
using BookingFlightApp.ReadModels;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookingFlightApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PassengerController : ControllerBase
    {
        private readonly Entities _entities;

        public PassengerController(Entities entities)
        {
            _entities = entities;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register([FromBody] NewPassengerDto dto)
        {
            _entities.Passengers.Add(new Passenger(dto.Email, dto.Password, dto.Name, dto.Gender));
            _entities.SaveChanges();

            return CreatedAtAction(nameof(Find), new { email = dto.Email });
        }


        [HttpGet("{email}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public ActionResult<PassengerRm> Find(string email)
        {
            var passenger = _entities.Passengers.FirstOrDefault(pas => pas.Email == email);
            if (passenger == null)
            {
                return NotFound();
            }

            var passengerRm = new PassengerRm(
                passenger.Email,
                passenger.Name,
                passenger.Gender
            );

            return Ok(passengerRm);
        }
    }
}
