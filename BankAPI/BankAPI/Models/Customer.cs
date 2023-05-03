namespace BankAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public Address Address { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}