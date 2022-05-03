using Async_Inn.Data;
using Async_Inn.Models.DTO;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class HotelRoomServices : IHotelRoom
    {
        private readonly AsyncInnDbContext _context;

        public HotelRoomServices(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<HotelRoomDTO> AddRoomToHotel(int hotelId,HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Added;

            await _context.SaveChangesAsync();

            HotelRoomDTO room = new HotelRoomDTO
            {
                HotelId = hotelId,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.RoomId,
                PetFriendly = hotelRoom.PetFrienndly,
                RoomID = hotelRoom.RoomId
            };

            return room;
        }

        public async Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId)
        {
            return await _context.HotelRooms
               .Where(hr => hr.HotelId == hotelId)
               .Select(hr => new HotelRoomDTO
               {
                   HotelId = hr.HotelId,
                   Rate = hr.Rate,
                   RoomID = hr.RoomId,
                   RoomNumber = hr.RoomNumber,
                   Room = new RoomDTO
                   {
                       Id = hr.Room.Id,
                       Name = hr.Room.Name,
                       Layout = hr.Room.Layout,
                       Amenities = hr.Room.RoomAmenities
                           .Select(a => new AmenityDTO
                           {
                               Id = a.Amenity.Id,
                               Name = a.Amenity.Name
                           }).ToList()
                   }
               }).ToListAsync();
        }
        public async Task<HotelRoom> RoomDetails(int hotelId, int roomNumber)
        {
            HotelRoom roomDetails = await _context.HotelRooms
                .Where(hr => hr.HotelId == hotelId && hr.RoomNumber == roomNumber)
                .FirstAsync();

            HotelRoom hotelRoom = await _context.HotelRooms.Include(r => r.Room)
                                                           .ThenInclude(am => am.RoomAmenities)
                                                           .ThenInclude(a => a.Amenity)
                                                           .Where(h => h.HotelId == roomDetails.HotelId && h.RoomId == roomDetails.RoomId)
                                                           .FirstAsync();
            return hotelRoom;
        }

        public async Task<HotelRoom> UpdateRoomDetails(int hotelId, int roomNumber, HotelRoom hr)
        {
            HotelRoom roomDetails = new HotelRoom
            {
                HotelId = hotelId,
                RoomNumber = roomNumber,
                RoomId = hr.RoomId,
                Rate = hr.Rate,
                PetFrienndly = hr.PetFrienndly
            };

            _context.Entry(roomDetails).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return hr;
        }

        public async Task DeleteRoomFromHotel(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms
                .Where(hr => hr.HotelId == hotelId && hr.RoomNumber == roomNumber)
                .FirstAsync();

            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

    }
}
