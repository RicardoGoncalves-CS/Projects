using BankApp.API.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BankApp.API.Services;

public class BankService<T> : IBankService<T> where T : class
{
    protected readonly IBankRepository<T> _repository;

    public BankService(IBankRepository<T> respository)
    {
        _repository = respository;
    }

    public async Task<bool> CreateAsync(T entity)
    {
        if (_repository.IsEmpty || entity == null)
        {
            return false;
        }
        else
        {
            _repository.Add(entity);
            await _repository.SaveAsync();
            return true;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (_repository.IsEmpty)
        {
            return false;
        }

        var entity = await _repository.FindAsync(id);

        if (entity == null)
        {
            return false;
        }

        _repository.Remove(entity);

        await _repository.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<T>?> GetAllAsync()
    {

        if (_repository.IsEmpty)
        {
            return null;
        }
        return (await _repository.GetAllAsync())
            .ToList();
    }

    public async Task<T?> GetAsync(int id)
    {
        if (_repository.IsEmpty)
        {
            return null;
        }

        T entity = await _repository.FindAsync(id);

        if (entity == null)
        {

            return null;
        }

        return entity;
    }

    public async Task SaveAsync()
    {
        await _repository.SaveAsync();
    }

    public async Task<bool> UpdateAsync(int id, T entity)
    {
        if (entity == null)
        {
            return false;
        }

        _repository.Update(entity);

        try
        {
            await _repository.SaveAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
        return true;
    }
}
