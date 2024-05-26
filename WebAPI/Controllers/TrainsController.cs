using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ApplicationServices.implementations;
using ApplicationServices.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Data.context;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TrainsController : ControllerBase
    {
        private TrainManagementService service;

        public TrainsController(TrainsDbContext context)
        {
            service = new TrainManagementService(context);
        }

        // GET: api/Trains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainDTO>>> GetTrains()
        {
            return await service.GetAllTrains();
        }

        // GET: api/Trains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainDTO>> GetTrain(int id)
        {
            var train = await service.GetTrain(id);

            if (train == null)
            {
                return NotFound();
            }

            return train;
        }

        // PUT: api/Trains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrain(int id, Train train)
        {
            if (id != train.Id)
            {
                return BadRequest();
            }

            await service.PutTrain(id, new()
            {
                Id = train.Id,
                Name = train.Name,
                Type = train.Type,
                Capacity = train.Capacity,
                ManufactureDate = train.ManufactureDate,
                MaxSpeed = train.MaxSpeed,
            });

            return NoContent();
        }

        // POST: api/Trains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Train>> PostTrain(Train train)
        {
            await service.PostTrain(new()
            {
                Id = train.Id,
                Name = train.Name,
                Type = train.Type,
                Capacity = train.Capacity,
                ManufactureDate = train.ManufactureDate,
                MaxSpeed = train.MaxSpeed,
            });

            return CreatedAtAction("GetTrain", new { id = train.Id }, train);
        }

        // DELETE: api/Trains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrain(int id)
        {
            await service.DeleteTrain(id);

            return NoContent();
        }

        private bool TrainExists(int id)
        {
            return service._context.Trains.Any(e => e.Id == id);
        }
    }
}
