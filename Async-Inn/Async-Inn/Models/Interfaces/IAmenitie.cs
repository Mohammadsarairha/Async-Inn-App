using Async_Inn.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    public interface IAmenitie
    {
        Task<AmenityDTO> Create(Amenity amenity);

        Task<List<AmenityDTO>> GetAmenities();

        Task<AmenityDTO> GetAmenity(int id);

        Task<AmenityDTO> UpdateAmenity(int id, AmenityDTO amenity);

        Task DeleteAmenity(int id);
    }
}
