namespace Bank.API.Models.DTOs.CustomerDTOs
{
    public class CreateCustomerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public int AddressId { get; set; }
    }
}
