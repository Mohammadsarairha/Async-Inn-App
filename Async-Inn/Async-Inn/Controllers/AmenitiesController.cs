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
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenitie _amenity;

        public AmenitiesController(IAmenitie amenity)
        {
            _amenity = amenity ;
        }

        // GET: api/Amenities
        [Authorize(Policy ="create")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenityDTO>>> GetAmenities()
        {
            var amenities = await _amenity.GetAmenities();

            return Ok(amenities);
        }

        // GET: api/Amenities/5
        [Authorize(Policy = "create")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AmenityDTO>> GetAmenity(int id)
        {
            var amenity = await _amenity.GetAmenity(id);

            return Ok(amenity);
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "update")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenity(int id, Amenity amenity)
        {
            if (id != amenity.Id)
            {
                return BadRequest();
            }

            AmenityDTO modifiedAmenity = await _amenity.UpdateAmenity(id, amenity);

            return Ok(modifiedAmenity);
        }

        // POST: api/Amenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "create")]
        [HttpPost]
        public async Task<ActionResult<AmenityDTO>> PostAmenity(Amenity amenity)
        {
            AmenityDTO newAmenity = await _amenity.Create(amenity);

            return Ok(newAmenity);
        }

        // DELETE: api/Amenities/5
        [Authorize(Policy = "delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            await _amenity.DeleteAmenity(id);

            return NoContent();
        }
    }
}
