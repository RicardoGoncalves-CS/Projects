using Bank.API.Data.Repository;
using Bank.API.Models;
using Bank.API.Models.DTOs.AddressDTOs;

namespace Bank.API.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Address> CreateAddressAsync(CreateAddressDTO addressDTO)
        {
            var address = new Address
            {
                No = addressDTO.No,
                Street = addressDTO.Street,
                City = addressDTO.City,
                PostCode = addressDTO.PostCode,
                Country = addressDTO.Country
            };

            await _addressRepository.AddAsync(address);

            return address;
        }
    }
}
