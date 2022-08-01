using AirlineReservationSystem.Models;
using AirlineReservationSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirlineReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        private readonly AirportService _airportService;

        public AirportsController(AirportService airportService)
        {
            _airportService = airportService;
        }
        [HttpGet]
        public IActionResult GetAirportsContext()
        {
            if (_airportService == null)
            {
                return NotFound();
            }
            return Ok(_airportService.GetAllAirports());
        }

        [HttpGet("{id}")]
        public IActionResult GetAirports(string id)
        {
            if (_airportService == null)
            {
                return NotFound();
            }
            var airports = _airportService.GetAirportById(id);

            if (airports == null)
            {
                return NotFound();
            }
            return Ok(airports);
        }

        [HttpPost]
        public IActionResult AddNewAirport(Airports airports)
        {
            if (_airportService == null)
            {
                return Problem("Entity set 'FlyWithFalconDBContext.AirportsContext'  is null.");
            }
            int val = _airportService.AddNewAirport(airports);
            if (val != 200)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetAirports", new { id = airports.AirportID }, airports);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAirports(string id, Airports airports)
        {
            if (id != airports.AirportID)
            {
                return BadRequest();
            }

            if (_airportService == null)
            {
                return Problem("Entity set 'FlyWithFalconDBContext.AirportsContext'  is null.");
            }

            int val = _airportService.UpdateAirports(airports);
            if (val != 200)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetAirports", new { id = airports.AirportID }, airports);
        }
    }
}
