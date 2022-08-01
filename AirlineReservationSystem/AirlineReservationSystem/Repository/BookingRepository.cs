using AirlineReservationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineReservationSystem.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HawksAvaitionDBContext _dbContext;
        private readonly IExceptionRepository _exceptionServices;

        public BookingRepository(HawksAvaitionDBContext dbContext, IExceptionRepository exceptionServices)
        {
            _dbContext = dbContext;
            _exceptionServices = exceptionServices;
        }

        public int CancelBookingbyId(int Id)
        {
           
            int response = StatusCodes.Status400BadRequest; ;
            Bookings booking;
            try
            {
                booking = _dbContext.Bookings
                      .FirstOrDefault(c => c.BookingID == Id);

                if (booking == null)
                {
                    response = StatusCodes.Status404NotFound;
                }
                var flight = _dbContext.Flights
                        .FirstOrDefault(c => c.FlightNo == booking.FlightNo);
                
                booking.Status = "Cancelled";
                flight.AvailableSeats = flight.AvailableSeats + booking.Seats;
                _dbContext.Bookings.Update(booking);
                _dbContext.Flights.Update(flight);
                _dbContext.SaveChanges();
                response = StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {
                
            }
            return response;
        }

        public int CreateNewBooking(Bookings bookings)
        {
            var flight = _dbContext.Flights
                        .FirstOrDefault(c => c.FlightNo == bookings.FlightNo);
            int reponse = StatusCodes.Status501NotImplemented;
            try
            {
                if (bookings.Seats > flight.AvailableSeats)
                {
                    reponse = StatusCodes.Status403Forbidden;
                    return reponse;
                }
                bookings.BookingAmount = bookings.Seats * flight.Fare;
                bookings.Arrival = flight.Arrival;
                bookings.Departure = flight.Departure;
                bookings.BookingID = 0;
                flight.AvailableSeats = flight.AvailableSeats - bookings.Seats;
                bookings.Status = "In Progress";
                _dbContext.Bookings.Add(bookings);
                _dbContext.Flights.Update(flight);
                _dbContext.SaveChanges();
                bookings.Status = "Booked";
                _dbContext.Bookings.Update(bookings);
                _dbContext.SaveChanges();
                reponse = StatusCodes.Status201Created;
            }
            catch (Exception ex)
            {
                //_exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {
                
            }
            return reponse;
        }

        public List<Bookings> GetAllBookings()
        {

            List<Bookings> bookings = null ;

            try
            {
                bookings = _dbContext.Bookings.AsNoTracking()
                                              .ToList();
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {
                
            }
            return bookings;
        }

        public List<Bookings> GetAllCustBookings(int custId)
        {

            List<Bookings> bookings = null;

            try
            {
                bookings = _dbContext.Bookings.AsNoTracking()
                        .Where(c => c.CustomerID == custId)
                                              .ToList();
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {

            }
            return bookings;
        }

        public Bookings GetBookingById(int Id)
        {
            

            Bookings booking = null;

            try
            {
                booking = _dbContext.Bookings
                                    .FirstOrDefault(c => c.BookingID == Id);
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {
                
            }
            return booking;
        }

        public int UpdateBooking(Bookings bookings)
        {


            int response = StatusCodes.Status501NotImplemented;

            try
            {
                Bookings bookings1 = _dbContext.Bookings
                .AsNoTracking()
                .FirstOrDefault(c => c.BookingID == bookings.BookingID);
                
                if (bookings1 != null)
                {
                    _dbContext.Bookings.Update(bookings);
                    _dbContext.SaveChanges();
                    response = StatusCodes.Status200OK;
                }
                else
                {
                    response = StatusCodes.Status404NotFound;
                }
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {
               
            }

            return response;
        }

        public int CheckInBooking(int Id)
        {
            int response = StatusCodes.Status400BadRequest;
            Bookings booking;
            try
            {
                booking = _dbContext.Bookings
                      .FirstOrDefault(c => c.BookingID == Id);

                if (booking == null)
                {
                    response = StatusCodes.Status404NotFound;
                }

                booking.Status = "CheckedIn";
                _dbContext.Bookings.Update(booking);
                _dbContext.SaveChanges();
                response = StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {

            }
            return response;
        }
    }
}
