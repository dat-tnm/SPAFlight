using BookingFlightApp.DTOs;
using BookingFlightApp.Domain.Entities;
using BookingFlightApp.ReadModels;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BookingFlightApp.Error;
using BookingFlightApp.Data;

namespace BookingFlightApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly ILogger<FlightController> _logger;
        private readonly Entities _entities;


        public FlightController(ILogger<FlightController> logger,
            Entities entities)
        {
            _logger = logger;
            _entities = entities;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(IEnumerable<FlightRm>), 200)]
        public IEnumerable<FlightRm> Search()
        {
            var flightRms = _entities.Flights.Select(f => new FlightRm(
                    f.Id,
                    f.Airline,
                    f.Price,
                    new TimePlaceRm(f.Departure.Place, f.Departure.Time),
                    new TimePlaceRm(f.Arrival.Place, f.Arrival.Time),
                    f.RemainingNumberOfSeats
                )).ToArray();

            return flightRms;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(FlightRm), 200)]

        public ActionResult<FlightRm> Find(Guid id)
        {
            var flight = _entities.Flights.SingleOrDefault(f => f.Id == id);

            if (flight == null)
                return NotFound();

            var flightRm = new FlightRm(
                    flight.Id,
                    flight.Airline,
                    flight.Price,
                    new TimePlaceRm(flight.Departure.Place, flight.Departure.Time),
                    new TimePlaceRm(flight.Arrival.Place, flight.Arrival.Time),
                    flight.RemainingNumberOfSeats
                    );

            return Ok(flight);
        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public IActionResult Book(BookDto dto)
        {
            var flight = _entities.Flights.SingleOrDefault(f => f.Id == dto.FlightId);

            if (flight == null)
            {
                return NotFound();
            }

            var error = flight.MakingBook(dto.PassengerEmail, dto.NumberOfSeats);

            if (error is OverBooking)
            {
                return Conflict(new { message = "Not enough seats!" });
            }

            _entities.SaveChanges();

            return CreatedAtAction(nameof(Find), new { id = dto.FlightId });
        }
    }
}