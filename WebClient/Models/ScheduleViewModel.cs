using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }
        [Required]
        public int TrainId { get; set; }
        public virtual TrainViewModel Train { get; set; }
        [Required]
        public int StationId { get; set; }
        public virtual StationViewModel Station { get; set; }
        public DateTime ArrivalTime { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }
        public int Platform { get; set; }
    }
}
