using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class RoomServices : IRoom
    {
        private readonly AsyncInnDbContext _context;

        public RoomServices(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<Room> Create(Room room)
        {
            _context.Entry(room).State = EntityState.Added;

            await _context.SaveChangesAsync();
            return room;
        }
       
        public async Task<Room> GetRoom(int id)
        {
            //Room room = await _context.Rooms.FindAsync(id);
            //return room;

            return await _context.Rooms.Include(ra => ra.RoomAmenities)
                                       .ThenInclude(a => a.Amenity)
                                       .FirstAsync(x => x.Id == id);
        }

        public async Task<List<Room>> GetRooms()
        {
            //var rooms = await _context.Rooms.ToListAsync();
            //return rooms;

            return await _context.Rooms.Include(ra => ra.RoomAmenities)
                                       .ThenInclude(a => a.Amenity)
                                       .ToListAsync();
        }

        public async Task<Room> UpdateRoom(int id, Room room)
        {
            _context.Entry(room).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return room;
        }

        public async Task DeleteRoom(int id)
        {
            Room room = await GetRoom(id);

            _context.Entry(room).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            RoomAmenity roomAmenity = new RoomAmenity
            {
                RoomId = roomId,
                AmenityId = amenityId
            };

            _context.Entry(roomAmenity).State = EntityState.Added;

            await _context.SaveChangesAsync();

        }

        public async Task RemoveAmentityFromRoom(int roomId, int amenityId)
        {
            RoomAmenity roomAmenity = await _context.RoomAmenities
                                            .Where(Rm => Rm.RoomId == roomId && Rm.AmenityId == amenityId)
                                            .FirstAsync();
            _context.Entry(roomAmenity).State = EntityState.Deleted;
            _context.SaveChanges();
        }
    }
}
