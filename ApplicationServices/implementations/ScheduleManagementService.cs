using ApplicationServices.DTOs;
using Data.context;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.implementations
{
    public class ScheduleManagementService : IScheduleManagementService
    {
        public readonly TrainsDbContext _context;
        public ScheduleManagementService(TrainsDbContext context)
        {
            _context = context;
        }
        public async Task DeleteSchedule(int id)
        {
            var schedules = await _context.Schedules.ToListAsync();

            foreach (var schedule in schedules)
            {
                if (schedule.Id == id)
                {
                    _context.Schedules.Remove(schedule);
                    _context.SaveChanges();
                    return;
                }
            }
        }
        public async Task<List<ScheduleDTO>> GetAllSchedules()
        {
            List<ScheduleDTO> schedulesDTOs = new List<ScheduleDTO>();
            var schedules = await _context.Schedules.ToArrayAsync();

            foreach (var schedule in schedules)
            {
                schedulesDTOs.Add(new()
                {
                    Id = schedule.Id,
                    TrainId = schedule.TrainId,
                    StationId = schedule.StationId,
                    ArrivalTime = schedule.ArrivalTime,
                    DepartureTime = schedule.DepartureTime,
                    Platform = schedule.Platform,
                });
            }
            return schedulesDTOs;
        }
        public async Task<ScheduleDTO?> GetSchedule(int id)
        {
            var schedules = await _context.Schedules.ToListAsync();

            foreach (var schedule in schedules)
            {
                if (schedule.Id == id)
                {
                    return new()
                    {
                        Id = schedule.Id,
                        TrainId = schedule.TrainId,
                        StationId = schedule.StationId,
                        ArrivalTime = schedule.ArrivalTime,
                        DepartureTime = schedule.DepartureTime,
                        Platform = schedule.Platform,
                    };
                }
            }
            return null; //IDK WHAT TO DO HERE ASK FOR HELP YOU DUMB IDIOT 
        }
        public async Task PostSchedule(ScheduleDTO scheduleDTO)
        {
            await _context.Schedules.AddAsync(new()
            {
                Id = scheduleDTO.Id,
                TrainId = scheduleDTO.TrainId,
                StationId = scheduleDTO.StationId,
                ArrivalTime = scheduleDTO.ArrivalTime,
                DepartureTime = scheduleDTO.DepartureTime,
                Platform = scheduleDTO.Platform,
            });
            await _context.SaveChangesAsync();
        }
        public async Task PutSchedule(int id, ScheduleDTO scheduleDTO)
        {
            var result = await _context.Schedules.SingleOrDefaultAsync(t => t.Id == id);
            if (result != null)
            {
                result.Id = scheduleDTO.Id;
                result.TrainId = scheduleDTO.TrainId;
                result.StationId = scheduleDTO.StationId;
                result.ArrivalTime = scheduleDTO.ArrivalTime;
                result.DepartureTime = scheduleDTO.DepartureTime;
                result.Platform = scheduleDTO.Platform;
                await _context.SaveChangesAsync();
            }
        }
    }
}
