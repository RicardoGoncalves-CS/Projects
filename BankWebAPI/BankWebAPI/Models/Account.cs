using System.ComponentModel.DataAnnotations.Schema;

namespace BankWebAPI.Models;

public class Account
{
    public int Id { get; set; }
    public int AccountNo { get; set; }
    public decimal Balance { get; set; }
    public DateTime OpenDate { get; set; }
    public bool IsActive { get; set; }

    public int CustomerId { get; set; }
    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; }

    public int BranchId { get; set; }
    [ForeignKey("BranchId")]
    public Branch Branch { get; set; }
}
