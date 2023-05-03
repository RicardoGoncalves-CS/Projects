namespace Bank.API.Models.DTOs.BranchDTOs
{
    public class CreateBranchDTO
    {
        public string BranchName { get; set; }
        public int AddressId { get; set; }
    }
}