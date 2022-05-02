using Async_Inn.Data;
using Async_Inn.Models.DTO;
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

        public async Task<RoomDTO> Create(Room room)
        {
            _context.Entry(room).State = EntityState.Added;

            await _context.SaveChangesAsync();

            RoomDTO roomDTO = new RoomDTO
            {
                Id = room.Id,
                Name = room.Name,
                Layout = room.Layout
            };
            
            return roomDTO;
        }
       
        public async Task<RoomDTO> GetRoom(int id)
        {
            //Room room = await _context.Rooms.FindAsync(id);
            //return room;

            return await _context.Rooms.Select(r => new RoomDTO
            {
                Id = r.Id,
                Name = r.Name,
                Layout = r.Layout,
                Amenities = r.RoomAmenities.Select(a => new AmenityDTO
                {
                    Id = a.Amenity.Id,
                    Name = a.Amenity.Name
                }).ToList()
            }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<RoomDTO>> GetRooms()
        {
            //var rooms = await _context.Rooms.ToListAsync();
            //return rooms;

            //return await _context.Rooms.Include(ra => ra.RoomAmenities)
            //                           .ThenInclude(a => a.Amenity)
            //                           .ToListAsync();

            return await _context.Rooms.Select(r => new RoomDTO
            {
                Id = r.Id,
                Name = r.Name,
                Layout = r.Layout,
                Amenities = r.RoomAmenities.Select(a => new AmenityDTO
                {
                    Id = a.Amenity.Id,
                    Name = a.Amenity.Name
                }).ToList()
            }).ToListAsync();
        }

        public async Task<RoomDTO> UpdateRoom(int id, Room room)
        {
            RoomDTO roomDTO = new RoomDTO
            {
                Id = room.Id,
                Name = room.Name,
                Layout = room.Layout
            };

            _context.Entry(room).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return roomDTO;
        }

        public async Task DeleteRoom(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);

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
