using Bank.API.Models;

namespace Bank.API.Data.Repository
{
    public interface IBranchRepository
    {
        Task AddAsync(Branch entity);
        Task<IEnumerable<Branch>> GetAllAsync();
        Task<Branch> GetByIdAsync(int id);
        Task RemoveAsync(Branch entity);
        Task UpdateAsync(Branch entity);
        Task<bool> ExistsAsync(int id);
        Task SaveAsync();
    }
}
