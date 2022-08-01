using AirlineReservationSystem.Models;
using Microsoft.EntityFrameworkCore;


namespace AirlineReservationSystem.Repository
{
    public class FlightRepository : IFlightRepository
    {
        private readonly HawksAvaitionDBContext _dbContext;
        private readonly IExceptionRepository _exceptionServices;

        public FlightRepository(HawksAvaitionDBContext dbContext, IExceptionRepository exceptionServices)
        {
            _dbContext = dbContext;
            _exceptionServices = exceptionServices;
        }
        public List<Flights> SearchFlights(string start, string dest, DateTime arrival)
        {

            List<Flights> flights;

            try
            {
                flights = _dbContext.Flights
                                .AsNoTracking()
                                .Where(f =>
                                f.Arrival.Date == arrival.Date
                                &&
                                f.Start == start
                                &&
                                f.Destination == dest
                                )
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
            return flights;
        }

        public int AddNewFlight(Flights flight)
        {
            int response = StatusCodes.Status501NotImplemented;
            try
            {
                _dbContext.Flights.Add(flight);
                _dbContext.SaveChanges();
                response = StatusCodes.Status200OK;
            }
            catch (Exception ex)
            {
                //_exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {

            }
            return response;
        }


        public List<Flights> GetAllFlights()
        {
            List<Flights> flight = null;
            try
            {
                flight = _dbContext.Flights.AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {

            }
            return flight;
        }

        public Flights GetFlightById(int id)
        {
            Flights flight = null;
            try
            {
                flight = _dbContext.Flights.FirstOrDefault(c => c.FlightNo == id);
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            finally
            {

            }
            return flight;
        }

        public int UpdateFlight(Flights flight)
        {
            int response = StatusCodes.Status501NotImplemented;
            try
            {
                Flights fli = _dbContext.Flights
                    .AsNoTracking()
                    .FirstOrDefault(c => c.FlightNo == flight.FlightNo);
                if (fli != null)
                {
                    _dbContext.Flights.Update(flight);
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

        public int DeleteFlight(int flightno)
        {
            int response = StatusCodes.Status400BadRequest;
            Flights fli;
            try
            {
                fli = _dbContext.Flights.FirstOrDefault(c => c.FlightNo == flightno);
                if (fli == null)
                {
                    response = StatusCodes.Status404NotFound;
                }
                else
                {
                    _dbContext.Flights.Remove(fli);
                    _dbContext.SaveChanges();
                    response = StatusCodes.Status200OK;
                }
            }
            catch (Exception ex)
            {
                _exceptionServices.CreateLog(ex, null);
                throw ex;
            }
            return response;
        }
    }
}
