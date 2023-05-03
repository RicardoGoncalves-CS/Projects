using Microsoft.EntityFrameworkCore;

namespace BankAPI.Data.Repository
{
    public class BankRepository<T> : IBankRepository<T> where T : class
    {
        private readonly DataContext _context;
        protected readonly DbSet<T> _dbSet;

        public BankRepository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public bool IsNull => _dbSet == null;

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual async Task<T?> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
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