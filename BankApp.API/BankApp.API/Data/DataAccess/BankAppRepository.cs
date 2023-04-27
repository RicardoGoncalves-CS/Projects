using Microsoft.EntityFrameworkCore;

namespace BankApp.API.Data.DataAccess
{
    public class BankAppRepository<T> : IBankAppRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _dbSet;

        public BankAppRepository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public bool IsNull => throw new NotImplementedException();

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public async Task<T?> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
