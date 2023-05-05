using Bank.API.Data.Repository;
using Bank.API.Models;
using Bank.API.Models.DTOs.CustomerDTOs;
using System.Net;

namespace Bank.API.Services
{
    public class CustomerService : IBankService<CreateCustomerDTO, Customer, UpdateCustomerDTO>
    {
        private readonly IBankRepository<Customer> _customerRepository;
        private readonly IBankRepository<Address> _addressRepository;
        private readonly IBankRepository<Account> _accountRepository;

        public CustomerService(IBankRepository<Customer> customerRepository, IBankRepository<Address> addressRepository, IBankRepository<Account> accountRepository)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _accountRepository = accountRepository;
        }

        public async Task<bool> CreateAsync(CreateCustomerDTO entity)
        {
            var address = await _addressRepository.GetByIdAsync(entity.AddressId);
            var accounts = await _accountRepository.GetAllAsync();

            var customer = new Customer
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Phone = entity.Phone,
                IsActive = entity.IsActive,
                Address = address,
                Accounts = accounts.Where(a => entity.AccountIds.Contains(a.Id)).ToList()
            };

            await _customerRepository.AddAsync(customer);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (_customerRepository == null)
            {
                return false;
            }

            var entity = await _customerRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            await _customerRepository.RemoveAsync(entity);

            return true;
        }

        public async Task<bool> EntityExists(int id)
        {
            return (await _customerRepository.GetByIdAsync(id)) != null;
        }

        public async Task<IEnumerable<Customer>?> GetAllAsync()
        {
            if (_customerRepository == null)
            {
                return null;
            }

            var entities = await _customerRepository.GetAllAsync();

            return entities;
        }

        public async Task<Customer?> GetAsync(int id)
        {
            if (_customerRepository == null)
            {
                return null;
            }

            var entity = await _customerRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task SaveAsync()
        {
            await _customerRepository.SaveAsync();
        }

        public async Task<bool> UpdateAsync(int id, UpdateCustomerDTO entity)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                throw new ArgumentException($"Customer with id {id} not found");
            }

            if (entity.AddressId != 0)
            {
                var address = await _addressRepository.GetByIdAsync(entity.AddressId);
                customer.Address = address;
            }

            var accounts = await _accountRepository.GetAllAsync();
            var Accounts = accounts.Where(a => entity.AccountIds.Contains(a.Id)).ToList();

            customer.FirstName = entity.FirstName;
            customer.LastName = entity.LastName;
            customer.Phone = entity.Phone;
            customer.IsActive = entity.IsActive;
            customer.Accounts = Accounts;

            await _customerRepository.UpdateAsync(customer);

            return true;
        }
    }
}
