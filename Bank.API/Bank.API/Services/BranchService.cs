using Bank.API.Data.Repository;
using Bank.API.Models;
using Bank.API.Models.DTOs.BranchDTOs;

namespace Bank.API.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBankRepository<Branch> _branchRepository;
        private readonly IBankRepository<Address> _addressRepository;
        private readonly IBankRepository<Customer> _customerRepository;

        public BranchService(IBankRepository<Branch> branchRepository, IBankRepository<Address> addressRepository, IBankRepository<Customer> customerRepository)
        {
            _branchRepository = branchRepository;
            _addressRepository = addressRepository;
            _customerRepository = customerRepository;
        }

        public async Task<bool> CreateAsync(CreateBranchDTO entity)
        {
            var address = await _addressRepository.GetByIdAsync(entity.AddressId);

            var branch = new Branch
            {
                BranchName = entity.BranchName,
                Address = address
            };

            await _branchRepository.AddAsync(branch);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (_branchRepository == null)
            {
                return false;
            }

            var entity = await _branchRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            await _branchRepository.RemoveAsync(entity);

            return true;
        }

        public async Task<bool> EntityExists(int id)
        {
            return (await _addressRepository.GetByIdAsync(id)) != null;
        }

        public async Task<IEnumerable<ReadBranchDTO>?> GetAllAsync()
        {
            if (_branchRepository == null)
            {
                return null;
            }

            var entities = await _branchRepository.GetAllAsync();

            var branchDTOs = new List<ReadBranchDTO>();

            foreach(var branch in entities)
            {
                var branchDTO = new ReadBranchDTO
                {
                    Id = branch.Id,
                    BranchName = branch.BranchName,
                    Address = branch.Address,
                    CustomerIds = branch.Customers?.Select(c => c.Id)
                };

                branchDTOs.Add(branchDTO);
            };

            return branchDTOs;
        }

        public async Task<ReadBranchDTO?> GetAsync(int id)
        {
            if(_branchRepository == null)
            {
                return null;
            }

            var branch = await _branchRepository.GetByIdAsync(id);
            if (branch == null)
            {
                return null;
            }

            var customerIds = branch.Customers?.Select(c => c.Id);

            var branchDTO = new ReadBranchDTO
            {
                Id = branch.Id,
                BranchName = branch.BranchName,
                Address = branch.Address,
                CustomerIds = customerIds
            };

            return branchDTO;
        }

        public async Task<bool> UpdateAsync(int id, UpdateBranchDTO entity)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            if (branch == null)
            {
                throw new ArgumentException($"Branch with id {id} not found");
            }

            if (entity.AddressId != 0)
            {
                var address = await _addressRepository.GetByIdAsync(entity.AddressId);
                branch.Address = address;
            }

            branch.BranchName = entity.BranchName;

            await _branchRepository.UpdateAsync(branch);

            return true;
        }

        public async Task SaveAsync()
        {
            await _branchRepository.SaveAsync();
        }

        public async Task<bool> AddCustomerToBranchAsync(int branchId, int customerId)
        {
            var branch = await _branchRepository.GetByIdAsync(branchId);
            if(branch == null)
            {
                return false;
            }

            var customer = await _customerRepository.GetByIdAsync(customerId);
            if(customer == null)
            {
                return false;
            }

            if (branch.Customers == null)
            {
                branch.Customers = new List<Customer>();
            }

            branch.Customers.Add(customer);

            return true;
        }
    }
}
