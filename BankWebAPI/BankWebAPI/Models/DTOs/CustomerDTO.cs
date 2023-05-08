namespace BankWebAPI.Models.DTOs
{
    public class CustomerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public int AddressId { get; set; }
        //public List<int> AccountIds { get; set; }
    }
}
