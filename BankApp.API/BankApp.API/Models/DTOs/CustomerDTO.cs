namespace BankApp.API.Models.DTOs
{
    // This represents the base DTO for customers
    // Doesn't include "AccountIds" as a newly created <Customer> doesn't have an account assigned yet
    // An <Account> can be assigned to a <Customer> through the AccountsController
    // To retrieve existing users <GetCustomerDTO> is used as it includes AccountIds
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public bool IsActive { get; set; }
        public int AddressId { get; set; }
    }
}
