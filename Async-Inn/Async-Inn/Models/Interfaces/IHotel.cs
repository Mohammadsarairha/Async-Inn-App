using Async_Inn.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotel
    {
        Task<HotelDTO> Create(Hotel hotel);

        Task<List<HotelDTO>> GetHotels();

        Task<HotelDTO> GetHotel(int id);

        Task<HotelDTO> UpdateHotel(int id , Hotel hotel);

        Task DeleteHotel(int id);
    }
}
