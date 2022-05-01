﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.DTO;

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
        // GET: api/HotelRooms/1/Rooms
        [HttpGet]
        [Route ("{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoomDTO>> GetHotelRooms(int hotelId)
        {
            var hotelRooms = await _HotelRoom.GetHotelRooms(hotelId);
            return Ok(hotelRooms);
        }

        // POST: api/HotelRooms/1/Rooms
        [HttpPost]
        [Route("{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoomDTO>> PostHotelRoom(int hotelId, HotelRoom hr)
        {
            var hotelRoom = await _HotelRoom.AddRoomToHotel(hotelId,hr);
            return Ok(hotelRoom);
        }
        // GET: api/HotelRooms/1/Rooms/1
        [HttpGet]
        [Route("{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> GetRoomDetails(int hotelId, int roomNumber)
        {
            var room = await _HotelRoom.RoomDetails(hotelId, roomNumber);

            return Ok(room);
        }

        // PUT: api/HotelRooms/1/Rooms/1
        [HttpPut]
        [Route("{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int roomNumber, HotelRoom hr)
        {
            var newRoom = await _HotelRoom.UpdateRoomDetails(hotelId,roomNumber,hr);
            return Ok(newRoom);
        }

        // DELETE: api/HotelRooms/5/1
        [HttpDelete]
        [Route("{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            await _HotelRoom.DeleteRoomFromHotel(hotelId, roomNumber);
            return NoContent();
        }
    }
}
