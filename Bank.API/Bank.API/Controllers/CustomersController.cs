using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bank.API.Data;
using Bank.API.Models;
using Bank.API.Models.DTOs.CustomerDTOs;
using Bank.API.Services;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IBankService<CreateCustomerDTO, Customer, UpdateCustomerDTO> _customerService;

        public CustomersController(DataContext context, IBankService<CreateCustomerDTO, Customer, UpdateCustomerDTO> customerService)
        {
            _context = context;
            _customerService = customerService;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customer = await _customerService.GetAllAsync();
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerService.GetAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, UpdateCustomerDTO customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            try
            {
                await _customerService.UpdateAsync(id, customer);
                await _customerService.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _customerService.EntityExists(id))
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
        public async Task<ActionResult<Customer>> PostCustomer(CreateCustomerDTO customerDTO)
        {
            bool created = await _customerService.CreateAsync(customerDTO);

            if (!created)
            {
                return Problem("There was a problem creating Branch.");
            }

            await _customerService.SaveAsync();

            return Ok("Customer successfully created.");
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerService.GetAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            await _customerService.DeleteAsync(id);
            await _customerService.SaveAsync();

            return NoContent();
        }
    }
}
