using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class TrainViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public string Type { get; set; }
        public int Capacity { get; set; }
        [Required]
        public DateTime ManufactureDate { get; set; }
        public double MaxSpeed { get; set; }
    }
}
