namespace Bank.API.Models.DTOs.BranchDTOs
{
    public class UpdateBranchDTO
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public int AddressId { get; set; }
        public List<int> CustomerIds { get; set; }
    }
}
