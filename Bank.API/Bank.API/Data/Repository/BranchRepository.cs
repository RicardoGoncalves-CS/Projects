using Bank.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.API.Data.Repository
{
    public class BranchRepository : IBankRepository<Branch>
    {
        private readonly DataContext _context;

        public BranchRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Branch entity)
        {
            _context.Branches.Add(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Branches.AnyAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Branch>> GetAllAsync()
        {
            return await _context.Branches
                .Include(b => b.Address)
                .Include(b => b.Customers)
                .ToListAsync();
        }

        public async Task<Branch> GetByIdAsync(int id)
        {
            return await _context.Branches
                .Include(b => b.Address)
                .Include(b => b.Customers)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task RemoveAsync(Branch entity)
        {
            _context.Branches.Remove(entity);
        }

        public async Task UpdateAsync(Branch entity)
        {
            _context.Branches.Update(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
