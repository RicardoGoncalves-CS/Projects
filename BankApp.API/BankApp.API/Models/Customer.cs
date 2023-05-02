using System.Security.Principal;

namespace BankApp.API.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public bool IsActive { get; set; }
        public Address Address { get; set; }
        //public Branch Branch { get; set; }
        public ICollection<Account>? Accounts { get; set; }
    }
}
