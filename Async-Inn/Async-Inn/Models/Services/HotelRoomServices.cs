using Async_Inn.Data;
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

        public async Task<HotelRoom> AddRoomToHotel(int hotelId,HotelRoom hotelRoom)
        {
            HotelRoom room = new HotelRoom
            {
                HotelId = hotelId,
                RoomNumber = hotelRoom.RoomNumber,
                RoomId = hotelRoom.RoomId,
                Rate = hotelRoom.Rate,
                PetFrienndly = hotelRoom.PetFrienndly
            };

            _context.Entry(room).State = EntityState.Added;

            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            return await _context.HotelRooms.Include(r => r.Room)
                                            .ThenInclude(hr => hr.RoomAmenities)
                                            .ThenInclude(ra => ra.Amenity)
                                            .Where(x => x.HotelId == hotelId)
                                            .ToListAsync();
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
