using ApplicationServices.DTOs;
using ApplicationServices.implementations;
using Data.context;
using Data.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Collections.Specialized.BitVector32;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
        private StationManagementService service;

        public StationsController(TrainsDbContext context)
        {
            service = new StationManagementService(context);
        }

        // GET: api/Trains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StationDTO>>> GetStations()
        {
            return await service.GetAllStation();
        }

        // GET: api/Trains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StationDTO>> GetStation(int id)
        {
            var station = await service.GetStation(id);

            if (station == null)
            {
                return NotFound();
            }

            return station;
        }

        // PUT: api/Trains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStation(int id, Station station)
        {
            if (id != station.Id)
            {
                return BadRequest();
            }

            await service.PutStation(id, new()
            {
                Id = station.Id,
                Name = station.Name,
                PlatformCount = station.PlatformCount,
                IsOperational = station.IsOperational,
            });

            return NoContent();
        }

        // POST: api/Trains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Station>> PostStation(Station station)
        {
            await service.PostStation(new()
            {
                Id = station.Id,
                Name = station.Name,
                PlatformCount = station.PlatformCount,
                IsOperational = station.IsOperational,
            });

            return CreatedAtAction("GetStation", new { id = station.Id }, station);
        }

        // DELETE: api/Trains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStation(int id)
        {
            await service.DeleteStation(id);

            return NoContent();
        }
    }
}
