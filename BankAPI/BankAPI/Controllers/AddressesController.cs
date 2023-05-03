using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankAPI.Data;
using BankAPI.Models;
using BankAPI.Models.AddressDTOs;
using BankAPI.Services;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IBankService<Address> _service;

        public AddressesController(IBankService<Address> service)
        {
            _service = service;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            //if (_context.Addresses == null)
            //{
            //    return NotFound();
            //}
            //return await _context.Addresses.ToListAsync();

            return (await _service.GetAllAsync())
                .ToList();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            //if (_context.Addresses == null)
            //{
            //    return NotFound();
            //}

            //var address = await _context.Addresses.FindAsync(id);

            //if (address == null)
            //{
            //    return NotFound();
            //}

            //return address;

            var address = await _service.GetAsync(id);

            if(address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            if (id != address.AddressId)
            {
                return BadRequest();
            }

            //_context.Entry(address).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!AddressExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            var updated = await _service.UpdateAsync(id, address);


            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(
            [Bind("No", "Street", "City", "PostCode", "Country")] CreateAddressDTO addressDTO)
        {
            //if (_context.Addresses == null)
            //{
            //    return Problem("Entity set 'DataContext.Addresses'  is null.");
            //}

            Address address = new Address
            {
                No = addressDTO.No,
                Street = addressDTO.Street,
                City = addressDTO.City,
                PostCode = addressDTO.PostCode,
                Country = addressDTO.Country
            };

            bool created = await _service.CreateAsync(address);

            //_context.Addresses.Add(address);
            //await _context.SaveChangesAsync();

            if (created == false)
            {
                return Problem("There was a problem creating Address.");
            }

            return CreatedAtAction("GetAddress", new { id = address.AddressId }, address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            //if (_context.Addresses == null)
            //{
            //    return NotFound();
            //}
            //var address = await _context.Addresses.FindAsync(id);
            //if (address == null)
            //{
            //    return NotFound();
            //}

            //_context.Addresses.Remove(address);
            //await _context.SaveChangesAsync();

            var deleted = await _service.DeleteAsync(id);

            if(deleted == false)
            {
                return NotFound();
            }

            return NoContent();
        }

        private async Task<bool> AddressExists(int id)
        {
            //return (_context.Addresses?.Any(e => e.AddressId == id)).GetValueOrDefault();
            return await _service.EntityExists(id);
        }
    }
}
