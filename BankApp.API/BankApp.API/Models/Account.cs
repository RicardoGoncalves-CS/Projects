namespace BankApp.API.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int Balance { get; set; }
        public Customer Customer { get; set; }
    }
}