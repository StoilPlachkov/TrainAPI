using ApplicationServices.DTOs;
using Data.context;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ApplicationServices.implementations
{
    public class StationManagementService : IStationManagementService
    {
        public readonly TrainsDbContext _context;
        public StationManagementService(TrainsDbContext context)
        {
            _context = context;
        }
        public async Task DeleteStation(int id)
        {
            var stations = await _context.Stations.ToListAsync();

            foreach (var station in stations)
            {
                if (station.Id == id)
                {
                    _context.Stations.Remove(station);
                    _context.SaveChanges();
                    return;
                }
            }
        }
        public async Task<List<StationDTO>> GetAllStation()
        {
            List<StationDTO> stationsDTOs = new List<StationDTO>();
            var stations = await _context.Stations.ToArrayAsync();

            foreach (var station in stations)
            {
                stationsDTOs.Add(new()
                {
                    Id = station.Id,
                    Name = station.Name,
                    PlatformCount = station.PlatformCount,
                    IsOperational = station.IsOperational,
                });
            }
            return stationsDTOs;
        }
        public async Task<StationDTO?> GetStation(int id)
        {
            var stations = await _context.Stations.ToListAsync();

            foreach (var station in stations)
            {
                if (station.Id == id)
                {
                    return new()
                    {
                        Id = station.Id,
                        Name = station.Name,
                        PlatformCount = station.PlatformCount,
                        IsOperational = station.IsOperational,
                    };
                }
            }
            return null; //IDK WHAT TO DO HERE ASK FOR HELP YOU DUMB IDIOT 
        }
        public async Task PostStation(StationDTO stationDTO)
        {
            await _context.Stations.AddAsync(new()
            {
                Id = stationDTO.Id,
                Name = stationDTO.Name,
                PlatformCount = stationDTO.PlatformCount,
                IsOperational = stationDTO.IsOperational,
            });
            await _context.SaveChangesAsync();
        }
        public async Task PutStation(int id, StationDTO stationDTO)
        {
            var result = await _context.Stations.SingleOrDefaultAsync(t => t.Id == id);
            if (result != null)
            {
                result.Id = stationDTO.Id;
                result.Name = stationDTO.Name;
                result.PlatformCount = stationDTO.PlatformCount;
                result.IsOperational = stationDTO.IsOperational;
                await _context.SaveChangesAsync();
            }
        }
    }
}
