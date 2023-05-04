namespace Bank.API.Data.Repository
{
    public interface IBankRepository<T>
    {
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task RemoveAsync(T entity);
        Task UpdateAsync(T entity);
        Task<bool> ExistsAsync(int id);
        Task SaveAsync();
    }
}
