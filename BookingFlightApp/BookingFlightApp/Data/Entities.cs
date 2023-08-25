using BookingFlightApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingFlightApp.Data
{
    public class Entities : DbContext
    {
        public DbSet<Flight> Flights { get; set; }

        public DbSet<Passenger> Passengers { get; set; }

        public Entities(DbContextOptions<Entities> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>().HasKey(f => f.Id);
            modelBuilder.Entity<Passenger>().HasKey(p => p.Email);

            modelBuilder.Entity<Flight>().OwnsOne(f => f.Departure);
            modelBuilder.Entity<Flight>().OwnsOne(f => f.Arrival);
        } 
    }
}
