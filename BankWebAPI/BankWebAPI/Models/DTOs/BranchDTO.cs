namespace BankWebAPI.Models.DTOs;

public class BranchDTO
{
    public string BranchName { get; set; }
    public int AddressId { get; set; }
    public List<int> AccountIds { get; set; }
}