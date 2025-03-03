using DemoDB.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoDB.DB
{
    public class DemoContext : DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options) { }
        public DbSet<GuessTheNumberUser> guessTheNumber { get; set; }
        public DbSet<BlackjackUser> blackjack { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { optionsBuilder.UseSqlite("Data Source=demo.db"); }
    }
}
