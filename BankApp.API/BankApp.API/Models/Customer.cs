using System.Security.Principal;

namespace BankApp.API.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public Address Adress { get; set; }
        public List<Account>? Accounts { get; set; }
    }
}
