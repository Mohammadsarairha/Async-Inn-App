using Async_Inn.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotelRoom
    {
        Task <List<HotelRoomDTO>> GetHotelRooms(int hotelId);

        Task<HotelRoomDTO> AddRoomToHotel(int hotelId,HotelRoom hr);

        Task<HotelRoom> RoomDetails(int hotelId, int roomNumber);

        Task<HotelRoom> UpdateRoomDetails(int hotelId ,int roomNumber, HotelRoom hr);

        Task DeleteRoomFromHotel(int hotelId, int roomNumber);

    }
}
