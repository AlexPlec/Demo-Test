using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.DB
{
    public class GameDataManager
    {
        private readonly DemoContext _context;

        public GameDataManager(DemoContext context)
        { _context = context; }

        public async Task<List<T>> GetAllAsync<T>() where T : class
        { return await _context.Set<T>().ToListAsync(); }

        public async Task SaveDataAsync<T>(T entity) where T : class
        {
            if (entity == null)
            { throw new ArgumentNullException(nameof(entity)); }

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task ShowDataAsync<T>() where T : class
        {
            var entities = await GetAllAsync<T>();

            foreach (var entity in entities)
            {
                string output = "";
                foreach (var property in entity.GetType().GetProperties())
                {
                    // Skip the "id" property
                    if (property.Name.ToLower() != "id")
                    { output += $"{property.Name}: {property.GetValue(entity)} "; }
                }
                Console.WriteLine(output);
            }
        }

        public async Task ShowDataStatisticAsync<T>() where T : class
        {
            var entities = await GetAllAsync<T>();

            if (typeof(T) == typeof(BlackjackUser))
            {

                int totalGames = entities.Count();
                int wins = entities.OfType<BlackjackUser>().Count(entity => entity.GameResult == "win");
                int losses = entities.OfType<BlackjackUser>().Count(entity => entity.GameResult == "lose");
                double winRate = (double)wins / totalGames * 100;

                Console.WriteLine("Blackjack User Statistics:");
                Console.WriteLine($"- Total Games Played: {totalGames}");
                Console.WriteLine($"- Wins: {wins}");
                Console.WriteLine($"- Losses: {losses}");
                Console.WriteLine($"- Win Rate: {winRate:F2}%");
            }
            else if (typeof(T) == typeof(GuessTheNumberUser))
            {

                int totalGames = entities.Count();
                int averageAttempts = (int)entities.OfType<GuessTheNumberUser>().Average(entity => entity.Attempts);

                Console.WriteLine("GuessTheNumber User Statistics:");
                Console.WriteLine($"- Total Games Played: {totalGames}");
                Console.WriteLine($"- Average Attempts: {averageAttempts}");
            }
            else
            { Console.WriteLine("Statistics not available for this model type."); }
        }

    }
}
