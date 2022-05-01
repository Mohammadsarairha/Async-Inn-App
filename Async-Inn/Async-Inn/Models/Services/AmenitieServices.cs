using Async_Inn.Data;
using Async_Inn.Models.DTO;
using Async_Inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class AmenitieServices : IAmenitie
    {
        private readonly AsyncInnDbContext _context;

        public AmenitieServices(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<AmenityDTO> Create(Amenity amenity)
        {
            AmenityDTO amenityDTO = new AmenityDTO
            {
                Id = amenity.Id,
                Name = amenity.Name,
            };

            _context.Entry(amenityDTO).State = EntityState.Added;

            await _context.SaveChangesAsync();
            return amenityDTO;
        }
        public async Task<AmenityDTO> GetAmenity(int id)
        {
            //Amenity amenity = await _context.Amenities.FindAsync(id);

            //return amenity;

            return await _context.Amenities.Select(a => new AmenityDTO
            {
                Id = a.Id,
                Name = a.Name,

            }).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<AmenityDTO>> GetAmenities()
        {
            //var amenities = await _context.Amenities.ToListAsync();

            //return amenities;

            return await _context.Amenities.Select(a => new AmenityDTO
            {
                Id = a.Id,
                Name = a.Name,

            }).ToListAsync();
        }

        public async Task<AmenityDTO> UpdateAmenity(int id, AmenityDTO amenity)
        {
            AmenityDTO amenityDTO = new AmenityDTO
            {
                Id = amenity.Id,
                Name = amenity.Name,
            };
            _context.Entry(amenityDTO).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return amenityDTO;
        }

        public async Task DeleteAmenity(int id)
        {
            Amenity amenity = await _context.Amenities.FindAsync(id);

            _context.Entry(amenity).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }
    }
}
