using Bank.API.Data.Repository;
using Bank.API.Models;
using Bank.API.Models.DTOs.AddressDTOs;

namespace Bank.API.Services
{
    public class OldAddressService : IAddressService
    {
        private readonly IBankRepository<Address> _addressRepository;

        public OldAddressService(IBankRepository<Address> addressRepository)
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
