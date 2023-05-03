namespace BankAPI.Models.BranchDTOs
{
    public class UpdateBranchDTO
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int AddressId { get; set; }
        public ICollection<int> CustomerIds { get; set; }
        public ICollection<int> AccountIds { get; set; }
    }
}