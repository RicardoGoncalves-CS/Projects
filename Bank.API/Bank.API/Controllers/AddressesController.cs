﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bank.API.Data;
using Bank.API.Models;
using Bank.API.Models.DTOs.AddressDTOs;
using Bank.API.Data.Repository;
using Bank.API.Services;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IBankRepository<Address> _addressRepository;
        private readonly IBankService<CreateAddressDTO, Address, Address> _addressService;

        public AddressesController(IBankRepository<Address> addressRepository, IBankService<CreateAddressDTO, Address, Address> addressService)
        {
            _addressRepository = addressRepository;
            _addressService = addressService;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            var addresses = await _addressRepository.GetAllAsync();
            if(addresses == null)
            {
                return NotFound();
            }

            return Ok(addresses);
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await _addressRepository.GetByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.Id)
            {
                return BadRequest();
            }

            try
            {
                await _addressRepository.UpdateAsync(address);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _addressRepository.ExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(CreateAddressDTO addressDTO)
        {
            bool created = await _addressService.CreateAsync(addressDTO);
            //var address = await _addressService.CreateAsync(addressDTO);
            if (created == false)
            {
                return Problem("There was a problem creating Address.");
            }

            return Ok("Address created successfully.");
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _addressRepository.GetByIdAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            await _addressRepository.RemoveAsync(address);

            return NoContent();
        }
    }
}
