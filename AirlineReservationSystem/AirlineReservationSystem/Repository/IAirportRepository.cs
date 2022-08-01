using AirlineReservationSystem.Models;

namespace AirlineReservationSystem.Repository
{
    public interface IAirportRepository
    {
        int AddNewAirport(Airports airports);
        List<Airports> GetAllAirports();
        Airports GetAirportById(string Id);
        int UpdateAirports(Airports airports);
    }
}
