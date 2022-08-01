﻿using AirlineReservationSystem.Models;
using AirlineReservationSystem.Repository;

namespace AirlineReservationSystem.Services
{
    public class AirportService
    {
        private readonly IAirportRepository _airportRepository;

        public AirportService(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }

        public int AddNewAirport(Airports airports)
        {
            return _airportRepository.AddNewAirport(airports);
        }

        public int UpdateAirports(Airports airports)
        {
            return _airportRepository.UpdateAirports(airports);
        }

        public List<Airports> GetAllAirports()
        {
            return _airportRepository.GetAllAirports();
        }

        public Airports GetAirportById(string Id)
        {
            return _airportRepository.GetAirportById(Id);
        }
    }
}
