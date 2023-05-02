namespace BankApp.API.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int AccountNo { get; set; }
        public int Balance { get; set; }
        public DateTime OpenDate { get; set; }
        public bool IsActive { get; set; }
        public Customer Customer { get; set; }
        public Branch Branch { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}