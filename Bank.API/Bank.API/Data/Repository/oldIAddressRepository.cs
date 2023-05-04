using Bank.API.Models;

namespace Bank.API.Data.Repository
{
    public interface oldIAddressRepository
    {
        Task<Address> GetByIdAsync(int id);
        Task<IEnumerable<Address>> GetAllAsync();
        Task AddAsync(Address entity);
        Task UpdateAsync(Address entity);
        Task RemoveAsync(Address entity);
        Task<bool> ExistsAsync(int id);
    }
}
