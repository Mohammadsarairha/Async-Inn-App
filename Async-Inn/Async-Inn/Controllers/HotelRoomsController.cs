using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.Interfaces;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _HotelRoom;

        public HotelRoomsController(IHotelRoom hotelRoom)
        {
            _HotelRoom = hotelRoom;
        }
        // GET: api/HotelRooms/1
        [HttpGet("{hotelId}")]
        public async Task<ActionResult<Hotel>> GetHotelRooms(int hotelId)
        {
            var hotelRooms = await _HotelRoom.GetHotelRooms(hotelId);
            return Ok(hotelRooms);
        }

        // POST: api/HotelRooms/5/HotelRoom
        [HttpPost("{hotelId}")]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(int hotelId, HotelRoom hr)
        {
            var hotelRoom = await _HotelRoom.AddRoomToHotel(hotelId, hr);
            return Ok(hotelRoom);
        }

        // DELETE: api/HotelRooms/5/1
        [HttpDelete("{hotelId}/{roomId}")]
        public async Task<IActionResult> DeleteHotelRoom(int hotelId, int roomId)
        {
            await _HotelRoom.DeleteRoomFromHotel(hotelId, roomId);
            return NoContent();
        }

        // GET: api/HotelRooms/1/1
        [HttpGet("{hotelId}/{roomNumber}")]
        public async Task<IActionResult> GetRoomDetails(int hotelId, int roomNumber)
        {
            var room = await _HotelRoom.RoomDetails(hotelId, roomNumber);
            return Ok(room);
        }

        // PUT: api/HotelRooms/1/1/Room
        [HttpPut("{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int roomNumber, HotelRoom hr)
        {
            var newRoom = await _HotelRoom.UpdateRoomDetails(roomNumber, hr);
            return Ok(newRoom);
        }
    }
}
