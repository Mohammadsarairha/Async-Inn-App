using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<HotelRoom> AddRoomToHotel(int hotelId, HotelRoom hr)
        {
            hr.HotelId = hotelId;
            _context.Entry(hr).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return hr;
        }

        public async Task DeleteRoomFromHotel(int hotelId, int roomId)
        {
            var hotelRoom = await _context.HotelRooms
                .Where(hr => hr.HotelId == hotelId && hr.RoomId == roomId)
                .FirstAsync();

            _context.Entry(hotelRoom).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Hotel> GetHotelRooms(int hotelId)
        {
            return await _context.Hotels
                .Include(h => h.HotelRooms)
                .ThenInclude(a => a.Room)
                .FirstOrDefaultAsync(h => h.Id == hotelId);
        }

        public async Task<Room> RoomDetails(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms
                .Where(hr => hr.HotelId == hotelId && hr.RoomNumber == roomNumber)
                .FirstAsync();

            var room = await _context.Rooms
                .Where(r => r.Id == hotelRoom.RoomId)
                .FirstAsync();

            return room;
        }


        public async Task<HotelRoom> UpdateRoomDetails(int roomNumber, HotelRoom hr)
        {
            _context.Entry(hr).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hr;
        }
    }
}
