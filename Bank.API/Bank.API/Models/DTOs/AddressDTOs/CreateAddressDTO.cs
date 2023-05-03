namespace Bank.API.Models.DTOs.AddressDTOs
{
    public class CreateAddressDTO
    {
        public string No { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
    }
}
