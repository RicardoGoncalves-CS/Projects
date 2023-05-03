using Bank.API.Models;
using Bank.API.Models.DTOs.AddressDTOs;
using Microsoft.EntityFrameworkCore;

namespace Bank.API.Data.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly DataContext _context;

        public AddressRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Address entity)
        {
            _context.Addresses.AddAsync(entity);
            await _context.SaveChangesAsync();
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
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Address entity)
        {
            _context.Addresses.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Addresses.AnyAsync(a => a.Id == id);
        }
    }
}
