namespace BankApp.API.Models.DTOs
{
    // This class extends <CustomerDTO> and includes "AccountIds"
    // It is used in Get methods to include the accounts associated with the respective <Customer>
    public class GetCustomerDTO : CustomerDTO
    {
        public ICollection<int> AccountIds { get; set; }
    }
}
