using Bank.API.Data.Repository;
using Bank.API.Models;
using Bank.API.Models.DTOs.AddressDTOs;
using Microsoft.EntityFrameworkCore;

namespace Bank.API.Services
{
    public class AddressService : IBankService<CreateAddressDTO, Address, Address>
    {
        private readonly IBankRepository<Address> _addressRepository;

        public AddressService(IBankRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<bool> CreateAsync(CreateAddressDTO entity)
        {
            var address = new Address
            {
                No = entity.No,
                Street = entity.Street,
                City = entity.City,
                PostCode = entity.PostCode,
                Country = entity.Country
            };

            await _addressRepository.AddAsync(address);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (_addressRepository == null)
            {
                return false;
            }

            var entity = await _addressRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            _addressRepository.RemoveAsync(entity);

            return true;
        }

        public async Task<bool> EntityExists(int id)
        {
            return (await _addressRepository.GetByIdAsync(id)) != null;
        }

        public async Task<IEnumerable<Address>?> GetAllAsync()
        {
            if (_addressRepository == null)
            {
                return null;
            }

            var entities = await _addressRepository.GetAllAsync();

            return entities;
        }

        public async Task<Address?> GetAsync(int id)
        {
            if (_addressRepository == null)
            {
                return null;
            }

            var entity = await _addressRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return null;
            }

            return entity;
        }

        public async Task<bool> UpdateAsync(int id, Address entity)
        {
            _addressRepository.UpdateAsync(entity);
            try
            {
                await _addressRepository.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await EntityExists(id)))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public async Task SaveAsync()
        {
            await _addressRepository.SaveAsync();
        }
    }
}