using Bank.API.Models;
using Bank.API.Models.DTOs.AddressDTOs;

namespace Bank.API.Services
{
    public interface oldIAddressService
    {
        Task<Address> CreateAddressAsync(CreateAddressDTO addressDTO);
    }
}