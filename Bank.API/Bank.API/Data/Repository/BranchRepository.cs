using Bank.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.API.Data.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly DataContext _context;

        public BranchRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Branch entity)
        {
            _context.Branches.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Branches.AnyAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Branch>> GetAllAsync()
        {
            return await _context.Branches.Include(b => b.Address).ToListAsync();
        }

        public async Task<Branch> GetByIdAsync(int id)
        {
            return await _context.Branches.Include(b => b.Address).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task RemoveAsync(Branch entity)
        {
            _context.Branches.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Branch entity)
        {
            _context.Branches.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
