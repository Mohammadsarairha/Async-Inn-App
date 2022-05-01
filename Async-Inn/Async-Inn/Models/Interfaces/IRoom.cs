using Async_Inn.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async_Inn.Models.Interfaces
{
    public interface IRoom
    {
        Task<RoomDTO> Create(Room room);

        Task<List<RoomDTO>> GetRooms();

        Task<RoomDTO> GetRoom(int id);

        Task<RoomDTO> UpdateRoom(int id, Room room);

        Task DeleteRoom(int id);

        Task AddAmenityToRoom(int roomId, int amenityId);

        Task RemoveAmentityFromRoom(int roomId, int amenityId);
    }
}
