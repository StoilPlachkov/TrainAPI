using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data
{
    public class Schedule : BaseEntity
    {
        [Required]
        public int TrainId { get; set; }
        public virtual Train Train { get; set; }
        [Required]
        public int StationId { get; set; }
        public virtual Station Station { get; set; }
        public DateTime ArrivalTime { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }
        public int Platform { get; set; }
    }
}
