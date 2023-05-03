using BankApp.API.Models.DTOs;
using BankApp.API.Models;
using BankApp.API.Models.DTOs.AccountDTOs;

namespace BankApp.API.Controllers
{
    public class Utils
    {
        public static AccountDTO AccountToDTO(Account account) => new AccountDTO
        {
            Id = account.Id,
            Balance = account.Balance,
            OpenDate = account.OpenDate,
            IsActive = account.IsActive,
            CustomerId = account.Customer.Id,
            BranchId = account.Branch.Id
        };

        public static GetAccountDTO GetAccountToDTO(Account account)
        {
            var accountDTO = new GetAccountDTO
            {
                Id = account.Id,
                Balance = account.Balance,
                OpenDate = account.OpenDate,
                IsActive = account.IsActive,
                CustomerId = account.Customer.Id,
                BranchId = account.Branch.Id,
                Transactions = new List<AccountTransactionsDTO>()
            };

            foreach(var transaction in account.Transactions)
            {
                accountDTO.Transactions.Add(Utils.TransactionToDTO(transaction));
            }

            return accountDTO;
        }

        public static GetCustomerDTO GetCustomerToDTO(Customer customer) => new GetCustomerDTO
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Phone = customer.Phone,
            DOB = customer.DOB,
            IsActive = customer.IsActive,
            AddressId = customer.Address.Id,
            AccountIds = customer.Accounts.Select(a => a.Id).ToList()
        };

        public static AccountTransactionsDTO TransactionToDTO(Transaction transaction) => new AccountTransactionsDTO
        {
            Id = transaction.Id,
            TransactionType = transaction.TransactionType,
            Amount = transaction.Amount,
            DateCreated = transaction.DateCreated,
        };

        public static GetBranchDTO GetBranchToDTO(Branch branch)
        {
            var branchDTO = new GetBranchDTO()
            {
                Id = branch.Id,
                Name = branch.Name,
                Address = branch.Address,
                CustomerIds = new List<int>(),
                AccountIds = new List<int>()
            };

            foreach(var customer in branch.Customers)
            {
                branchDTO.CustomerIds.Add(customer.Id);
            }

            foreach(var account in branch.Accounts)
            {
                branchDTO.AccountIds.Add(account.Id);
            }

            return branchDTO;
        }
    }
}
