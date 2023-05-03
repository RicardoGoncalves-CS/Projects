namespace BankApp.API.Models.DTOs.AccountDTOs
{
    public class AccountTransactionsDTO
    {
        public int Id { get; set; }
        public string TransactionType { get; set; }
        public int Amount { get; set; }
        public DateTime DateCreated { get; set; }
        //public int AccountId { get; set; }
    }
}
