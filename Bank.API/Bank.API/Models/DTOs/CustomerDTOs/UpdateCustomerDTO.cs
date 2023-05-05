namespace Bank.API.Models.DTOs.CustomerDTOs
{
    public class UpdateCustomerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public int AddressId { get; set; }
        public IEnumerable<int>? AccountIds { get; set; }
    }
}
