using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AirlineReservationSystem.Models;

namespace AirlineReservationSystem.Models
{
    public class HawksAvaitionDBContext : DbContext
    {
        public HawksAvaitionDBContext(DbContextOptions<HawksAvaitionDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        public DbSet<Bookings> Bookings { get; set; }
        public DbSet<Flights> Flights { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Airports> Airports { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<ExceptionLog> ExceptionLog { get; set; }
    }
}
