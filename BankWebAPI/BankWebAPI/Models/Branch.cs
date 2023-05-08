namespace BankWebAPI.Models;

public class Branch
{
    public int Id { get; set; }
    public string BranchName { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; }

    public ICollection<Account> Accounts { get; set; }
}
