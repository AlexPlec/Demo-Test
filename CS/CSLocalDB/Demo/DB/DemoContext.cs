using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.DB
{
    public class DemoContext : DbContext
    {
        public DbSet<GuessTheNumberUser> guessTheNumber { get; set; }
        public DbSet<BlackjackUser> blackjack { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { optionsBuilder.UseSqlite("Data Source=demo.db"); }
    }
}
