namespace Bank.API.Services
{
    public interface IBankService<TCreate, TRead, TUpdate>
    {
        Task<bool> CreateAsync(TCreate entity);
        Task<bool> UpdateAsync(int id, TUpdate entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TRead>?> GetAllAsync();
        Task<TRead?> GetAsync(int id);
        Task<bool> EntityExists(int id);
    }
}
