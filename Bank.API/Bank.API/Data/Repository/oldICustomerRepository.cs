using Bank.API.Models;

namespace Bank.API.Data.Repository
{
    public interface oldICustomerRepository
    {
        Task<Customer> GetByIdAsync(int id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task AddAsync(Customer entity);
        Task UpdateAsync(Customer entity);
        Task RemoveAsync(Customer entity);
        Task<bool> ExistsAsync(int id);
    }
}
