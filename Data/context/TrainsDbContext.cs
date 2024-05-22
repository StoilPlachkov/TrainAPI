using Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.context
{
    public class TrainsDbContext : DbContext
    {
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Train> Trains { get; set; }

        public TrainsDbContext(DbContextOptions<TrainsDbContext> options) : base(options)
        {

        }
    }
}
