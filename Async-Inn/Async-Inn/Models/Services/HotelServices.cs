using Async_Inn.Data;
using Async_Inn.Models.DTO;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class HotelServices : IHotel
    {
        private readonly AsyncInnDbContext _context;

        public HotelServices(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<HotelDTO> Create(Hotel hotel)
        {
            
            _context.Entry(hotel).State = EntityState.Added;
            
            await _context.SaveChangesAsync();

            HotelDTO hotelDTO = new HotelDTO
            {
                Id = hotel.Id,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,
            };

            return hotelDTO;
        }
        public async Task<HotelDTO> GetHotel(int id)
        {
            return await _context.Hotels.Select(
                hotel => new HotelDTO
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    StreetAddress = hotel.StreetAddress,
                    City = hotel.City,
                    State = hotel.State,
                    Phone = hotel.Phone,
                    Rooms = hotel.HotelRooms.Select(hotelR => new HotelRoomDTO
                    {
                        HotelId = hotelR.HotelId,
                        RoomNumber = hotelR.RoomNumber,
                        Rate = hotelR.Rate,
                        PetFriendly = hotelR.PetFrienndly,
                        RoomID = hotelR.RoomId,
                        Room = new RoomDTO
                        {
                            Id = hotelR.Room.Id,
                            Name = hotelR.Room.Name,
                            Layout = hotelR.Room.Layout,
                            Amenities = hotelR.Room.RoomAmenities
                            .Select(A => new AmenityDTO
                            {
                                Id = A.Amenity.Id,
                                Name = A.Amenity.Name
                            }).ToList()
                        }
                    }).ToList()
                }).FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<List<HotelDTO>> GetHotels()
        {
            //var hotels = await _context.Hotels.ToListAsync();

            //return hotels;
            return await _context.Hotels.Select(
                hotel => new HotelDTO
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    StreetAddress = hotel.StreetAddress,
                    City = hotel.City,
                    State = hotel.State,
                    Phone = hotel.Phone,
                    Rooms = hotel.HotelRooms.Select(hotelR => new HotelRoomDTO
                    {
                        HotelId = hotelR.HotelId,
                        RoomNumber = hotelR.RoomNumber,
                        Rate = hotelR.Rate,
                        PetFriendly = hotelR.PetFrienndly,
                        RoomID = hotelR.RoomId,
                        Room = new RoomDTO
                        {
                            Id = hotelR.Room.Id,
                            Name = hotelR.Room.Name,
                            Layout = hotelR.Room.Layout,
                            Amenities = hotelR.Room.RoomAmenities
                            .Select(A => new AmenityDTO
                            {
                                Id = A.Amenity.Id,
                                Name = A.Amenity.Name
                            }).ToList()
                        }
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<HotelDTO> UpdateHotel(int id, Hotel hotel)
        {
            HotelDTO hotelDTO = new HotelDTO
            {
                Id = hotel.Id,
                Name = hotel.Name,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,
            };

            _context.Entry(hotel).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return hotelDTO;
        }

        public async Task DeleteHotel(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);

            _context.Entry(hotel).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

    }
}
