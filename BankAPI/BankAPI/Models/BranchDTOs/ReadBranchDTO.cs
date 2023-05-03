namespace BankAPI.Models.BranchDTOs
{
    public class ReadBranchDTO
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public Address Address { get; set; }
        public ICollection<int> CustomerIds { get; set; }
        public ICollection<int> AccountIds { get; set; }
    }
}
