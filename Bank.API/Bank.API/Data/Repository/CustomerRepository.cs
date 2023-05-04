using Bank.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank.API.Data.Repository
{
    public class CustomerRepository : IBankRepository<Customer>
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer entity)
        {
            _context.Customers.AddAsync(entity);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .Include(c => c.Address)
                .ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await _context.Customers
                .Where(c => c.Id == id)
                .Include(c => c.Address)
                .FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(Customer entity)
        {
            _context.Customers.Remove(entity);
        }

        public async Task UpdateAsync(Customer entity)
        {
            _context.Customers.Update(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Customers.AnyAsync(a => a.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
