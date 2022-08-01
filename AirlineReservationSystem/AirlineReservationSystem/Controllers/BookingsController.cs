using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AirlineReservationSystem.Models;
using AirlineReservationSystem.Services;
using AirlineReservationSystem.DTOs;
using AirlineReservationSystem.Repository;

namespace AirlineReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly BookingService _bookingService;


        public BookingsController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult GetAllBookings()
        {
            if (_bookingService == null)
            {
                return NotFound();
            }
            return Ok(_bookingService.GetAllBookings());
        }

        [HttpGet("custbooking/{cid}")]
        public IActionResult GetAllCustomerBookings(int cid)
        {
            if (_bookingService == null)
            {
                return NotFound();
            }
            return Ok(_bookingService.GetAllCustomerBookings(cid));
        }

        [HttpGet("{id}")]
        public IActionResult GetBookings(int id)
        {
            if (_bookingService == null)
            {
                return NotFound();
            }
            var bookings = _bookingService.GetBookingById(id);

            if (bookings == null)
            {
                return NotFound();
            }
            return Ok(bookings);
        }

        [HttpPost]
        public IActionResult AddNewBooking(BookingCreateDTO bookingDto)
        {
            if (_bookingService == null)
            {
                return Problem("Entity set 'HawksAvaitionDBContext.BookingsContext'  is null.");
            }
            Bookings bookings = new Bookings();
            bookings.CustomerID = bookingDto.CustomerID;
            bookings.FlightNo = bookingDto.FlightNo;
            bookings.Seats = bookingDto.Seats;

            int val = _bookingService.AddNewBooking(bookings);
            if (val != 201)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetBookings", new { id = bookings.BookingID }, bookings);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBooking(int id, Bookings bookings)
        {
            if (id != bookings.BookingID)
            {
                return BadRequest();
            }

            if (_bookingService == null)
            {
                return Problem("Entity set 'HawksAvaitionDBContext.BookingsContext'  is null.");
            }

            int val = _bookingService.EditBooking(bookings);
            if (val != 200)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetBookings", new { id = bookings.BookingID }, bookings);
        }

        [HttpPut("/api/Bookings/CheckIn/{id}")]

        public IActionResult CheckInBooking(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            if (_bookingService == null)
            {
                return Problem("Entity set 'HawksAvaitionDBContext.BookingsContext'  is null.");
            }

            int val = _bookingService.CheckInBooking(id);
            if (val != 200)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult CancelBookings(int id)
        {
            if (_bookingService == null)
            {
                return NotFound();
            }
            int val = _bookingService.CancelBooking(id);
            if (val != 200)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
