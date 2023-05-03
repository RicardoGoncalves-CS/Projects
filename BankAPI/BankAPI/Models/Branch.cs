namespace BankAPI.Models
{
    public class Branch
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public Address Address { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Account> Accounts { get; set; }
    }
}
