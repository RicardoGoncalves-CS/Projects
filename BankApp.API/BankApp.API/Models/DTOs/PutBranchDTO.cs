namespace BankApp.API.Models.DTOs
{
    public class PutBranchDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public ICollection<int> Customers { get; set; }
        public ICollection<int> Accounts { get; set; }
    }
}
