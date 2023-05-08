using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankWebAPI.Data;
using BankWebAPI.Models;
using BankWebAPI.Models.DTOs;

namespace BankWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressesController : ControllerBase
{
    private readonly DataContext _context;

    public AddressesController(DataContext context)
    {
        _context = context;
    }

    // GET: api/Addresses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
    {
      if (_context.Addresses == null)
      {
          return NotFound();
      }
        return await _context.Addresses.ToListAsync();
    }

    // GET: api/Addresses/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Address>> GetAddress(int id)
    {
      if (_context.Addresses == null)
      {
          return NotFound();
      }
        var address = await _context.Addresses.FindAsync(id);

        if (address == null)
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
        if (id != address.Id)
        {
            return BadRequest();
        }

        _context.Entry(address).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AddressExists(id))
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
    public async Task<ActionResult<Address>> PostAddress(AddressDTO addressDTO)
    {
        if (_context.Addresses == null)
        {
            return Problem("Entity set 'DataContext.Addresses'  is null.");
        }

        Address address = new Address
        {
            No = addressDTO.No,
            Street = addressDTO.Street,
            City = addressDTO.City,
            PostCode = addressDTO.PostCode,
            Country = addressDTO.Country
        };
        
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAddress", new { id = address.Id }, address);
    }

    // DELETE: api/Addresses/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        if (_context.Addresses == null)
        {
            return NotFound();
        }
        var address = await _context.Addresses.FindAsync(id);
        if (address == null)
        {
            return NotFound();
        }

        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AddressExists(int id)
    {
        return (_context.Addresses?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
