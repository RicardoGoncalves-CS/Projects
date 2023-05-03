using BankAPI.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services
{
    public class BankService<T> : IBankService<T> where T : class
    {
        private readonly IBankRepository<T> _repository;

        public BankService(IBankRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateAsync(T entity)
        {
            if (_repository.IsNull)
            {
                return false;
            }

            _repository.Add(entity);
            await _repository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (_repository.IsNull)
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
            if (_repository.IsNull)
            {
                return null;
            }

            var entities = await _repository.GetAllAsync();

            return entities;
        }

        public async Task<T?> GetAsync(int id)
        {
            if (_repository.IsNull)
            {
                return null;
            }

            var entity = await _repository.FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task<bool> UpdateAsync(int id, T entity)
        {
            _repository.Update(entity);
            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await EntityExists(id)))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task<bool> EntityExists(int id)
        {
            return (await _repository.FindAsync(id)) != null;
        }
    }
}
