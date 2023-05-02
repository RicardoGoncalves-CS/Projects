using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankApp.API.Data;
using BankApp.API.Models;
using BankApp.API.Models.DTOs;

namespace BankApp.API.Controllers
{
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
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }

            var customers = await _context.Customers
                .Include(c => c.Accounts)
                .Include(c => c.Address)
                .ToListAsync();

            var customersDTO = new List<CustomerDTO>();

            foreach(var customer in customers)
            {
                customersDTO.Add(Utils.GetCustomerToDTO(customer));
            }

            return customersDTO;
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(int id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Where(c => c.Id == id)
                .Include(c => c.Accounts)
                .Include(c => c.Address)
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            return Utils.GetCustomerToDTO(customer);
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

            var address = await _context.Addresses.FindAsync(customerDTO.AddressId);
            if (address == null)
            {
                return Problem($"Address with Id {customerDTO.AddressId} doesn't exist.");
            }

            var accounts = await _context.Accounts
                .Where(a => a.Customer.Id == customerDTO.Id)
                .Include(a => a.Customer)
                .Include(a => a.Branch)
                .ToListAsync();

            Customer customer = new Customer
            {
                FirstName = customerDTO.FirstName,
                LastName = customerDTO.LastName,
                Phone = customerDTO.Phone,
                DOB = customerDTO.DOB,
                IsActive = customerDTO.IsActive,
                Address = address
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok("Customer successfully created");
            //return CreatedAtAction("GetCustomer", new { id = customer.Id }, customerDTO);
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
}
