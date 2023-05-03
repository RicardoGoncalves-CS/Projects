namespace BankAPI.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string No { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
    }
}