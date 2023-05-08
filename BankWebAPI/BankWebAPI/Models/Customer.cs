using System.ComponentModel.DataAnnotations.Schema;

namespace BankWebAPI.Models;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public bool IsActive { get; set; }

    public int AddressId { get; set; }
    [ForeignKey("AddressId")]
    public Address Address { get; set; }

    public ICollection<Account> Accounts { get; set; }
}
