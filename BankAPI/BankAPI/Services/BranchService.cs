using BankAPI.Data.Repository;
using BankAPI.Models;
using BankAPI.Models.BranchDTOs;
using System.Net;

namespace BankAPI.Services
{
    public class BranchService : IBankService<Branch>
    {
        private readonly BranchRepository _repository;

        public BranchService(BranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateAsync(Branch entity)
        {
            if (_repository.IsNull)
            {
                return false;
            }

            _repository.Add(entity);
            await _repository.SaveAsync();
            return true;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EntityExists(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Branch>?> GetAllAsync()
        {
            if (_repository.IsNull)
            {
                return null;
            }

            var entities = await _repository.GetAllAsync();

            return entities;
        }

        public Task<Branch?> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id, Branch entity)
        {
            throw new NotImplementedException();
        }
    }
}
