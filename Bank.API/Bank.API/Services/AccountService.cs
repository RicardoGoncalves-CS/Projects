using Bank.API.Data.Repository;
using Bank.API.Models;
using Bank.API.Models.DTOs.AccountDTOs;
using Bank.API.Models.DTOs.BranchDTOs;

namespace Bank.API.Services
{
    public class AccountService : IBankService<CreateAccountDTO, ReadAccountDTO, UpdateAccountDTO>
    {
        private readonly IBankRepository<Account> _accountRepository;
        private readonly IBankRepository<Branch> _branchRepository;
        private readonly IBankRepository<Customer> _customerRepository;

        public AccountService(IBankRepository<Account> accountRepository, IBankRepository<Branch> branchRepository, IBankRepository<Customer> customerRepository)
        {
            _accountRepository = accountRepository;
            _branchRepository = branchRepository;
            _customerRepository = customerRepository;
        }

        public async Task<bool> CreateAsync(CreateAccountDTO entity)
        {
            var branch = await _branchRepository.GetByIdAsync(entity.BranchId);
            var customer = await _customerRepository.GetByIdAsync(entity.CustomerId);

            var accout = new Account
            {
                AccountNo = entity.AccountNo,
                Balance = entity.Balance,
                OpenDate = entity.OpenDate,
                IsActive = entity.IsActive,
                Branch = branch,
                Customer = customer
            };

            await _accountRepository.AddAsync(accout);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (_accountRepository == null)
            {
                return false;
            }

            var entity = await _accountRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            await _accountRepository.RemoveAsync(entity);

            return true;
        }

        public async Task<bool> EntityExists(int id)
        {
            return (await _accountRepository.GetByIdAsync(id)) != null;
        }

        public async Task<IEnumerable<ReadAccountDTO>?> GetAllAsync()
        {
            if (_accountRepository == null)
            {
                return null;
            }

            var entities = await _accountRepository.GetAllAsync();

            var accountDTOs = new List<ReadAccountDTO>();

            foreach (var account in entities)
            {
                var accountDTO = new ReadAccountDTO
                {
                    Id = account.Id,
                    AccountNo = account.AccountNo,
                    Balance = account.Balance,
                    OpenDate = account.OpenDate,
                    IsActive = account.IsActive,
                    BranchId = account.Branch.Id,
                    CustomerId = account.Customer.Id
                };

                accountDTOs.Add(accountDTO);
            };

            return accountDTOs;
        }

        public async Task<ReadAccountDTO?> GetAsync(int id)
        {
            if (_accountRepository == null)
            {
                return null;
            }

            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                return null;
            }

            var accountDTO = new ReadAccountDTO
            {
                Id = account.Id,
                AccountNo = account.AccountNo,
                Balance = account.Balance,
                OpenDate = account.OpenDate,
                IsActive = account.IsActive,
                BranchId = account.Branch.Id,
                CustomerId = account.Customer.Id
            };

            return accountDTO;
        }

        public async Task SaveAsync()
        {
            await _accountRepository.SaveAsync();
        }

        public async Task<bool> UpdateAsync(int id, UpdateAccountDTO entity)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                throw new ArgumentException($"Account with id {id} not found");
            }

            var branch = await _branchRepository.GetByIdAsync(entity.BranchId);
            if (branch == null)
            {
                throw new ArgumentException($"Branch with id {entity.BranchId} not found");
            }

            var customer = await _customerRepository.GetByIdAsync(entity.CustomerId);
            if(customer == null)
            {
                throw new ArgumentException($"Customer with id {entity.CustomerId} not found");
            }

            account.AccountNo = entity.AccountNo;
            account.Balance = entity.Balance;
            account.IsActive = entity.IsActive;
            account.Branch = branch;
            account.Customer = customer;

            await _accountRepository.UpdateAsync(account);

            return true;
        }
    }
}
