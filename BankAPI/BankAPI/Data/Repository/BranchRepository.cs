using BankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Data.Repository
{
    public class BranchRepository : IBankRepository<Branch>
    {
        private readonly DataContext _context;
        protected readonly DbSet<Branch> _dbSet;

        public BranchRepository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<Branch>();
        }

        public bool IsNull => _dbSet == null;

        public async Task<IEnumerable<Branch>> GetAllAsync()
        {
            return await _dbSet
                .Include(b => b.Address)
                .Include(b => b.Customers)
                .Include(b => b.Accounts)
                .ToListAsync();
        }

        public async Task<Branch?> FindAsync(int id)
        {
            return await _dbSet
                .Where(b => b.BranchId == id)
                .Include(b => b.Address)
                .Include(b => b.Customers)
                .Include(b => b.Accounts)
                .FirstOrDefaultAsync();
        }

        public void Add(Branch entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<Branch> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Update(Branch entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(Branch entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
