namespace Bank.API.Models
{
    public class Branch
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public Address Address { get; set; }
        public ICollection<Customer>? Customers { get; set; }
    }
}
