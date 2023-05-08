namespace Bank.WebAPI.Models.DTOs
{
    public class BranchDTO
    {
        public string BranchName { get; set; }
        public int AddressId { get; set; }
        public IEnumerable<int> AccountIds { get; set; }
    }
}
