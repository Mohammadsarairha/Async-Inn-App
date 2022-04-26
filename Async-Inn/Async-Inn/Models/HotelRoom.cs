﻿namespace Async_Inn.Models
{
    public class HotelRoom
    {
        public int HotelId { get; set; }
        public int RoomNumber { get; set; }
        public int RoomId { get; set; }
        public decimal Rate { get; set; }
        public int PetFrienndly { get; set; }


        // Navigation Properties

        public Hotel Hotel { get; set; }
        public Room Room { get; set; }
    }
}
