using Microsoft.EntityFrameworkCore;

namespace BankApp.API.Data.Repositories;

public class BankRepository<T> : IBankRepository<T> where T : class
{
    private readonly BankContext _context;
    protected readonly DbSet<T> _dbSet;

    public BankRepository(BankContext context)
    {
        _dbSet = context.Set<T>();
        _context = context;
    }

    public bool IsEmpty => _dbSet == null;

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _dbSet.AddRange(entities);
    }

    public async Task<T?> FindAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }
}
