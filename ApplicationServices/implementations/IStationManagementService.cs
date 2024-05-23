using ApplicationServices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.implementations
{
    public interface IStationManagementService
    {
        Task<List<StationDTO>> GetAllStation();
        Task<StationDTO> GetStation(int id);
        Task PostStation(StationDTO stationDTO);
        Task PutStation(int id, StationDTO stationDTO);
        Task DeleteStation(int id);
    }
}
