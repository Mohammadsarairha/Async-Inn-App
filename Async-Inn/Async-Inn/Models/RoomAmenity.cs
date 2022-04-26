namespace Async_Inn.Models
{
    public class RoomAmenity
    {
        public int RoomId { get; set; }
        public int AmenityId { get; set; }

        // Navigation Properties

        public Room Room { get; set; }
        public Amenity Amenity { get; set; }
    }
}
