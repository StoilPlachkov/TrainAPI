using Data.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.DTOs
{
    public class ScheduleDTO : BaseDTO
    {
        [Required]
        public int TrainId { get; set; }
        [Required]
        public int StationId { get; set; }
        public DateTime ArrivalTime { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }
        public int Platform { get; set; }
    }
}
