using Microsoft.EntityFrameworkCore;
using ReversiMvcApp.Models;
using SpelApiClient;

namespace ReversiMvcApp.Data
{
    public class ReversiDbContext : DbContext
    {
        public ReversiDbContext(DbContextOptions<ReversiDbContext> options)
            : base(options)
        {
        }

        public DbSet<Speler> Spelers { get; set; }
        public DbSet<Spel> Spellen { get; set; } 
    }
}


