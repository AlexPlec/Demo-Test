using Microsoft.EntityFrameworkCore;

namespace DemoDB.DB
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
    }
}