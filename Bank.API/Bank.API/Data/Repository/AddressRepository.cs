using Bank.API.Models;
using Bank.API.Models.DTOs.AddressDTOs;
using Microsoft.EntityFrameworkCore;

namespace Bank.API.Data.Repository
{
    public class AddressRepository : IBankRepository<Address>
    {
        private readonly DataContext _context;

        public AddressRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Address entity)
        {
            await _context.Addresses.AddAsync(entity);
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            return await _context.Addresses.FindAsync(id);
        }

        public async Task RemoveAsync(Address entity)
        {
            _context.Addresses.Remove(entity);
        }

        public async Task UpdateAsync(Address entity)
        {
            _context.Addresses.Update(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Addresses.AnyAsync(a => a.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
