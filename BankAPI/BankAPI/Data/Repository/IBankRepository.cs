﻿namespace BankAPI.Data.Repository
{
    public interface IBankRepository<T>
    {
        bool IsNull { get; }
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> FindAsync(int id);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        Task SaveAsync();
    }
}
