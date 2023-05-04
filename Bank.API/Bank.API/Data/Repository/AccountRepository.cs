using Bank.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.API.Data.Repository
{
    public class AccountRepository : IBankRepository<Account>
    {
        private readonly DataContext _context;

        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Account entity)
        {
            await _context.Accounts.AddAsync(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Accounts.AnyAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task RemoveAsync(Account entity)
        {
            _context.Accounts.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Account entity)
        {
            _context.Accounts.Update(entity);
        }
    }
}
