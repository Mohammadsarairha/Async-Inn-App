using Async_Inn.Models;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Data
{
    public class AsyncInnDbContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }

        public AsyncInnDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This calls the base method, but does nothing
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Hotel>().HasData(
              new Hotel { Id = 1, Name = "The Westin Milwaukee", StreetAddress= "260-C North El Camino Rea", City= "Chicago", State= "Illinois", Country= "united state", Phone= "12163547758" },
              new Hotel { Id = 2, Name = "Wyndham Buena", StreetAddress = "591 Grand Avenue", City = "Los Angeles", State = "California", Country = "united state", Phone = "12099216581" },
              new Hotel { Id = 3, Name = "Monumental Movieland Hotel", StreetAddress = "1186 Roseville Pkwy", City = "Houston", State = "Texas", Country = "united state", Phone = "15042010052" }
            );

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Seahawks Snooze", Layout = 1 },
                new Room { Id = 2, Name = "Restful Rainier", Layout = 0 },
                new Room { Id = 3, Name = "Honeymoon suites", Layout = 2 }
                );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity { Id = 1, Name = "Coffee maker" },
                new Amenity { Id = 2, Name = "Ocean view" },
                new Amenity { Id = 3, Name = "Air conditioning" }
                );
        }
    }
}
