namespace BankApp.API.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string TransactionType { get; set; }
        public int Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public Account Account { get; set; }
    }
}