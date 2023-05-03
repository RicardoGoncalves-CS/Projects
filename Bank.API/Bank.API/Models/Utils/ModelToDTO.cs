using Bank.API.Models.DTOs.AddressDTOs;

namespace Bank.API.Models.Utils
{
    public static class ModelToDTO
    {
        public static CreateAddressDTO CreateAddressToDTO(Address address)
        {
            var addressDTO = new CreateAddressDTO
            {
                No = address.No,
                Street = address.Street,
                City = address.City,
                PostCode = address.PostCode,
                Country = address.Country
            };

            return addressDTO;
        }
    }
}
