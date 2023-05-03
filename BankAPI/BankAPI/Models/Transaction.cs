namespace BankAPI.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public Account Account { get; set; }
    }
}