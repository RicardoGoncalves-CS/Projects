namespace Bank.API.Models.DTOs.BranchDTOs
{
    public class GetBranchDTO
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public Address Address { get; set; }
        public IEnumerable<int>? CustomerIds { get; set; }
    }
}