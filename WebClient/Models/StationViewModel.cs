using System.ComponentModel.DataAnnotations;

namespace WebClient.Models
{
    public class StationViewModel
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int PlatformCount { get; set; }
        [Required]
        public bool IsOperational { get; set; }
    }
}
