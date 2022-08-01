using AirlineReservationSystem.Models;
using AirlineReservationSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly FlightService _flightService;
        public FlightsController(FlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet("{start}/{dest}/{doj}")]

        public IActionResult SearchFlights(string start, string dest, DateTime doj)
        {
            if (string.IsNullOrEmpty(dest) && string.IsNullOrEmpty(start))
            {
                return BadRequest(); 
            }
            return Ok(_flightService.SearchFlight(start, dest, doj));
        }

        [HttpGet]
        public IActionResult GetFlightsContext()
        {
            if (_flightService == null)
            {
                return NotFound();
            }
            return Ok(_flightService.GetAllFlights());
        }

        [HttpGet("{flightno}")]
        public IActionResult GetFlight(int flightno)
        {
            if (_flightService == null)
            {
                return NotFound();
            }
            var fli = _flightService.GetFlightById(flightno);

            if (fli == null)
            {
                return NotFound();
            }
            return Ok(fli);
        }



        [HttpPost]
        public IActionResult AddNewFlight(Flights flight)
        {
            if (_flightService == null)
            {
                return Problem("Entity set 'PeekAirwaysDBContext.FlightsContext'  is null.");
            }
            int val = _flightService.AddFlight(flight);
            if (val != 200)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetFlight", new { flightno = flight.FlightNo }, flight);
        }

        [HttpPut("{flightno}")]
        public IActionResult UpdateFlight(int flightno, Flights flight)
        {
            if (flightno != flight.FlightNo)
            {
                return BadRequest();
            }

            if (_flightService == null)
            {
                return Problem("Entity set 'PeekAirwaysDBContext.FlightsContext'  is null.");
            }

            int val = _flightService.EditFlights(flight);
            if (val != 200)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetFlight", new { flightno = flight.FlightNo }, flight);
        }

        [HttpDelete("{flightno}")]
        public IActionResult DeleteFlight(int flightno)
        {
            if (_flightService == null)
            {
                return NotFound();
            }
            int val = _flightService.DelFlight(flightno);
            if (val != 200)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
