using ApplicationServices.DTOs;
using ApplicationServices.implementations;
using Data.context;
using Data.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private ScheduleManagementService service;

        public SchedulesController(TrainsDbContext context)
        {
            service = new ScheduleManagementService(context);
        }

        // GET: api/Trains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleDTO>>> GetSchedules()
        {
            return await service.GetAllSchedules();
        }

        // GET: api/Trains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleDTO>> GetSchedule(int id)
        {
            var schedule = await service.GetSchedule(id);

            if (schedule == null)
            {
                return NotFound();
            }

            return schedule;
        }

        // PUT: api/Trains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, ScheduleDTO schedule)
        {
            if (id != schedule.Id)
            {
                return BadRequest();
            }

            await service.PutSchedule(id, new()
            {
                Id = schedule.Id,
                TrainId = schedule.TrainId,
                StationId = schedule.StationId,
                ArrivalTime = schedule.ArrivalTime,
                DepartureTime = schedule.DepartureTime,
                Platform = schedule.Platform,
            });

            return NoContent();
        }

        // POST: api/Trains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ScheduleDTO>> PostSchedule(ScheduleDTO schedule)
        {
            await service.PostSchedule(new()
            {
                Id = schedule.Id,
                TrainId = schedule.TrainId,
                StationId = schedule.StationId,
                ArrivalTime = schedule.ArrivalTime,
                DepartureTime = schedule.DepartureTime,
                Platform = schedule.Platform,
            });

            return CreatedAtAction("GetSchedule", new { id = schedule.Id }, schedule);
        }

        // DELETE: api/Trains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            await service.DeleteSchedule(id);

            return NoContent();
        }
    }
}
