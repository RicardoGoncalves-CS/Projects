namespace BankApp.API.Models.DTOs
{
    public class GetBranchDTO : BranchDTO
    {
        public Address Address { get; set; }
        public ICollection<int> CustomerIds { get; set; }
        public ICollection<int> AccountIds { get; set; }
    }
}
