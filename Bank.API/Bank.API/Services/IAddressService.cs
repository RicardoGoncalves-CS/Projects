using Bank.API.Models;
using Bank.API.Models.DTOs.AddressDTOs;

namespace Bank.API.Services
{
    public interface IAddressService
    {
        Task<Address> CreateAddressAsync(CreateAddressDTO addressDTO);
    }
}