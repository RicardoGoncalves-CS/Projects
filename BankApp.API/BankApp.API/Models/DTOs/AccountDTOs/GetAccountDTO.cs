using System.Reflection.Metadata.Ecma335;

namespace BankApp.API.Models.DTOs.AccountDTOs
{
    public class GetAccountDTO : AccountDTO
    {
        public ICollection<AccountTransactionsDTO> Transactions { get; set; } = new List<AccountTransactionsDTO>();
    }
}
