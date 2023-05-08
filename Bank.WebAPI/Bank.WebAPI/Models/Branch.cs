using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Bank.WebAPI.Models;

public class Branch
{
    public int Id { get; set; }
    public string BranchName { get; set; }
    public int AddressId { get; set; }

    [ForeignKey("AddressId")]
    public Address Address { get; set; }

    [NotMapped]
    public IEnumerable<int> AccountIds { get; set; }

    public ICollection<Account> Accounts { get; set; }
}
