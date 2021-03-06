using Async_Inn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Async_Inn.Data
{
    public class AsyncInnDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }

        public AsyncInnDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This calls the base method, but does nothing
            base.OnModelCreating(modelBuilder);

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
            SeedRoles(modelBuilder, "DistrictManager", "create","update","delete","read");
            SeedRoles(modelBuilder, "PropertyManager", "create", "update","read");
            SeedRoles(modelBuilder, "Agent", "update","read");
            SeedRoles(modelBuilder, "AnonymousUsers", "read");

            modelBuilder.Entity<RoomAmenity>().HasKey(
                roomAmenity => new {roomAmenity.RoomId , roomAmenity.AmenityId}
                );

            modelBuilder.Entity<HotelRoom>().HasKey(
                hotelRoom => new { hotelRoom.HotelId, hotelRoom.RoomId }
                );
        }

        private int id = 1;
        private void SeedRoles(ModelBuilder modelBuilder ,string roleName , params string[] permissions)
        {
            var role = new IdentityRole
            {
                Id =roleName.ToLower(), 
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };
            modelBuilder.Entity<IdentityRole>().HasData(role);
            var RoleClaims = permissions.Select(permissions => 
            new IdentityRoleClaim<string>
            {
                Id = id++,
                RoleId = role.Id,
                ClaimType= "persmissions",
                ClaimValue = permissions
            }).ToArray();

            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(RoleClaims);
        }
    }
}
