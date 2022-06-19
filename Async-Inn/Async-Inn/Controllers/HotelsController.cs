using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.Services;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotel _hotel;

        public HotelsController(IHotel hotel)
        {
            _hotel = hotel;
        }

        // GET: api/Hotels
        [Authorize(Policy = "read")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetHotels()
        {
            var hotels = await _hotel.GetHotels();

            return Ok(hotels);
        }

        // GET: api/Hotels/5
        [Authorize(Policy = "read")]
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            HotelDTO hotel = await _hotel.GetHotel(id);

            return Ok(hotel);
        }

        // PUT: api/Hotels/5
        [Authorize(Policy = "update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            HotelDTO modifiedHotel = await _hotel.UpdateHotel(id, hotel);

            return Ok(modifiedHotel);
        }

        // POST: api/Hotels
        [Authorize(Policy = "create")]
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            HotelDTO newHotel = await _hotel.Create(hotel);

            return Ok(newHotel);
        }

        // DELETE: api/Hotels/5
        [Authorize(Policy = "delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            await _hotel.DeleteHotel(id);

            return NoContent();
        }
    }
}
