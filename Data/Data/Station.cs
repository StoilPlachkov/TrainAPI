using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Data
{
    public class Station : BaseEntity
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int PlatformCount { get; set; }
        [Required]
        public bool IsOperational { get; set; }
    }
}
