using BankApp.API.Models;
using BankApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.API.Controllers;

[Route("api/Customers")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IBankService<Customer> _service;

    public CustomersController(IBankService<Customer> service)
    {
        _service = service;
    }

    // GET: api/Customers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        var result = await _service.GetAllAsync();
        if (result is null)
        {
            return NotFound();
        }
        return result.ToList();
    }

    // GET: api/Customers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(int id)
    {
        var result = await _service.GetAsync(id);
        if (result is null)
        {
            return NotFound();
        }
        return result;
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
        var result = await _service.UpdateAsync(id, customer);
        if (!result) return BadRequest();
        return NoContent();
    }

    // POST: api/Customers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
    {
        var result = await _service.CreateAsync(customer);
        if (!result)
        {
            return BadRequest("Error creating Customer");
        }
        return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
    }

    // DELETE: api/Customers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
