namespace BankAPI.Models.AddressDTOs
{
    public class CreateAddressDTO
    {
        // To use on the POST method; Excludes the AddressId property
        public string No { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
    }
}
