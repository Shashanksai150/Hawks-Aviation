﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirlineReservationSystem.Models
{
    public class Flights
    {
        
        int _flightNo = 0;
        [Key]
        public int FlightNo 
        { 
            get { return _flightNo; } 
            set { _flightNo = value; } 
        }

        string? _flightId;
        public string? FlightId
        {
            get { return _flightId; }
            set { _flightId = value; }
        }

        string? _flightName;
        public string? Name
        {
            get { return _flightName; }
            set { _flightName = value; }
        }

        
        string? _start;
        [ForeignKey("Airports")]
        public string? Start
        {
            get { return _start; }
            set { _start = value; }
        }

        
        string? _destination;
        [ForeignKey("Airports")]
        public string? Destination
        {
            get { return _destination; }
            set { _destination = value; }
        }

        DateTime _arrival;
        public DateTime Arrival 
        {
            get { return _arrival; }
            set { _arrival = value; }
        }

        DateTime _departure;
        public DateTime Departure
        {
            get { return _departure; }
            set { _departure = value; }
        }

        int _totalSeats;
        public int TotalSeats
        {
            get { return _totalSeats; }
            set { _totalSeats = value; }
        }

        int _availableSeats;
        public int AvailableSeats
        {
            get { return _availableSeats; }
            set
            {
                _availableSeats = value;
            }
        }

        double _fare;
        public double Fare
        {
            get { return _fare; } 
            set { _fare = value; }
        }
    }
}