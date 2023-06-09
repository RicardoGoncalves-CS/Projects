﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankWebAPI.Data;
using BankWebAPI.Models;
using BankWebAPI.Models.DTOs;

namespace BankWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly DataContext _context;

    public CustomersController(DataContext context)
    {
        _context = context;
    }

    // GET: api/Customers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
      if (_context.Customers == null)
      {
          return NotFound();
      }
        return await _context.Customers.ToListAsync();
    }

    // GET: api/Customers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(int id)
    {
      if (_context.Customers == null)
      {
          return NotFound();
      }
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        return customer;
    }

    // PUT: api/Customers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(int id, Customer customer)
    {
        if (id != customer.Id)
        {
            return BadRequest();
        }

        _context.Entry(customer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CustomerExists(id))
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

    // POST: api/Customers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Customer>> PostCustomer(CustomerDTO customerDTO)
    {
        if (_context.Customers == null)
        {
            return Problem("Entity set 'DataContext.Customers'  is null.");
        }

        var customer = new Customer
        {
            FirstName = customerDTO.FirstName,
            LastName = customerDTO.LastName,
            Phone = customerDTO.Phone,
            IsActive = customerDTO.IsActive,
            AddressId = customerDTO.AddressId,
            Address = await _context.Addresses.FindAsync(customerDTO.AddressId),
        };

        //foreach (var accountId in customerDTO.AccountIds)
        //{
        //    var account = await _context.Accounts.FindAsync(accountId);

        //    if (accountId == 0)
        //    {
        //        return Problem($"Account with ID {customerDTO.AccountIds} not found.");
        //    }

        //    customer.Accounts.Add(account);
        //}

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
    }

    // DELETE: api/Customers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        if (_context.Customers == null)
        {
            return NotFound();
        }
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CustomerExists(int id)
    {
        return (_context.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}
