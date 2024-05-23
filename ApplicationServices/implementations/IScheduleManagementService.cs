using ApplicationServices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.implementations
{
    public interface IScheduleManagementService
    {
        Task<List<ScheduleDTO>> GetAllSchedules();
        Task<ScheduleDTO> GetSchedule(int id);
        Task PostSchedule(ScheduleDTO ScheduleDTO);
        Task PutSchedule(int id, ScheduleDTO ScheduleDTO);
        Task DeleteSchedule(int id);
    }
}
