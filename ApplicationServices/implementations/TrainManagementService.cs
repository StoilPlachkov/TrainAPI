using ApplicationServices.DTOs;
using Data.context;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.implementations
{
    public class TrainManagementService : ITrainManagmentService
    {
        public readonly TrainsDbContext _context;
        public TrainManagementService(TrainsDbContext context) {
            _context = context; 
        }
        public async Task DeleteTrain(int id)
        {
            var trains = await _context.Trains.ToListAsync();

            foreach (var train in trains)
            {
                if (train.Id == id)
                {
                    _context.Trains.Remove(train);
                    _context.SaveChanges();
                    return;
                }
            }
        }

        public async Task<List<TrainDTO>> GetAllTrains()
        {
            List<TrainDTO> trainsDTOs = new List<TrainDTO>();
            var trains = await _context.Trains.ToArrayAsync();

            foreach (var train in trains)
            {
                trainsDTOs.Add(new()
                {
                    Id = train.Id,
                    Name = train.Name,
                    Type = train.Type,
                    ManufactureDate = train.ManufactureDate,
                    MaxSpeed = train.MaxSpeed,
                    Capacity = train.Capacity,
                });
            }
            return trainsDTOs;
        }

        public async Task<TrainDTO?> GetTrain(int id)
        {
            var trains = await _context.Trains.ToListAsync();

            foreach (var train in trains)
            {
                if (train.Id == id)
                {
                    return new()
                    {
                        Id = train.Id,
                        Name = train.Name,
                        Type = train.Type,
                        ManufactureDate = train.ManufactureDate,
                        MaxSpeed = train.MaxSpeed,
                        Capacity = train.Capacity,
                    };
                }
            }
            return null; //IDK WHAT TO DO HERE ASK FOR HELP YOU DUMB IDIOT 
        }

        public async Task PostTrain(TrainDTO trainDTO)
        {
            await _context.Trains.AddAsync(new()
            {
                Id = trainDTO.Id,
                Name = trainDTO.Name,
                Type = trainDTO.Type,
                Capacity = trainDTO.Capacity,
                ManufactureDate = trainDTO.ManufactureDate,
                MaxSpeed = trainDTO.MaxSpeed,
            }) ;
            await _context.SaveChangesAsync();
        }

        public async Task PutTrain(int id, TrainDTO trainDTO)
        {
            var result = await _context.Trains.SingleOrDefaultAsync(t => t.Id == id);
            if (result != null)
            {
                result.Name = trainDTO.Name;
                result.Type = trainDTO.Type;
                result.Capacity = trainDTO.Capacity;
                result.ManufactureDate = trainDTO.ManufactureDate;
                result.MaxSpeed = trainDTO.MaxSpeed;
                await _context.SaveChangesAsync();
            }
        }
    }
}
