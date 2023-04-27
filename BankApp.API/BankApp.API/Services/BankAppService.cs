using BankApp.API.Data.DataAccess;
using System.Data;

namespace BankApp.API.Services
{
    public class BankAppService<T> : IBankAppService<T> where T : class
    {
        private readonly IBankAppRepository<T> _repository;

        public BankAppService(IBankAppRepository<T> repository )
        {
            _repository = repository;
        }

        public async Task<bool> CreateAsync(T entity)
        {
            if (_repository.IsNull || entity == null)
            {
                return false;
            }
            else
            {
                _repository.Add(entity);
                return true;
            }
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
            return (await _repository.GetAllAsync())
                .ToList();
        }

        public async Task<T?> GetAsync(int id)
        {
            if (_repository.IsNull)
            {
                return null;
            }

            T? entity = await _repository.FindAsync(id);

            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public Task SaveAsync()
        {
            return _repository.SaveAsync();
        }

        public async Task<bool> UpdateAsync(int id, T entity)
        {
            if(await _repository.FindAsync(id) == null)
            {
                return false;
            }

            _repository.Update(entity);

            try
            {
                await _repository.SaveAsync();
            }
            catch (DBConcurrencyException)
            {
                return false;
            }

            return true;
        }
    }
}
