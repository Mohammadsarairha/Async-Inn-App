using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    public interface IHotelRoom
    {
        //    Task<HotelRoom> AddRoomToHotel(int roomId , HotelRoom hotelRoom);

        //    Task<Hotel> GetHotelRooms(int hotelId);

        //    Task<Room> RoomDetails(int hotelId, int roomNumber);

        //    Task<HotelRoom> UpdateHotel(int roomId, HotelRoom hotelRoom);

        //    Task DeleteRoomFromHotel(int hotelId, int roomId);

        Task<Hotel> GetHotelRooms(int hotelId);

        Task<HotelRoom> AddRoomToHotel(int hotelId, HotelRoom hr);

        Task<Room> RoomDetails(int hotelId, int roomNumber);

        Task<HotelRoom> UpdateRoomDetails(int roomNumber, HotelRoom hr);

        Task DeleteRoomFromHotel(int hotelId, int roomId);

    }
}
