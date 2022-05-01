namespace Async_Inn.Models.DTO
{
    public class HotelRoomDTO
    {
        public int HotelId { get; set; }
        public int RoomNumber { get; set; }
        public decimal Rate { get; set; }
        public int PetFriendly { get; set; }
        public int RoomID { get; set; }
        public RoomDTO Room { get; set; }
    }
}
