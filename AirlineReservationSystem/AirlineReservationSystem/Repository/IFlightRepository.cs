using AirlineReservationSystem.Models;

namespace AirlineReservationSystem.Repository
{
    public interface IFlightRepository
    {
        int AddNewFlight(Flights flight);
        List<Flights> GetAllFlights();
        Flights GetFlightById(int id);
        int UpdateFlight(Flights flight);
        int DeleteFlight(int flightno);
        List<Flights> SearchFlights(string start, string dest, DateTime arrival);
    }
}
