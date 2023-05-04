using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bank.API.Models;
using Bank.API.Data.Repository;
using Bank.API.Services;
using Bank.API.Models.DTOs.BranchDTOs;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBankRepository<Branch> _branchRepository;
        private readonly IBankService<CreateBranchDTO, GetBranchDTO, UpdateBranchDTO> _branchService;
        private readonly IBankRepository<Customer> _customerRepository;

        public BranchesController(IBankRepository<Branch> branchRepository, IBankService<CreateBranchDTO, GetBranchDTO, UpdateBranchDTO> branchService, IBankRepository<Customer> customerRepository)
        {
            _branchRepository = branchRepository;
            _branchService = branchService;
            _customerRepository = customerRepository;
        }

        // GET: api/Branches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBranchDTO>>> GetBranches()
        {
            var branches = await _branchService.GetAllAsync();
            if(branches == null)
            {
                return NotFound();
            }

            return Ok(branches);
        }

        // GET: api/Branches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetBranchDTO>> GetBranch(int id)
        {
            var branch = await _branchService.GetAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            return Ok(branch);
        }

        // PUT: api/Branches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBranch(int id, UpdateBranchDTO branchDTO)
        {
            if (id != branchDTO.Id)
            {
                return BadRequest();
            }

            try
            {
                await _branchService.UpdateAsync(id, branchDTO);
                await _branchService.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _branchService.EntityExists(id))
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
        /*
        // PUT: api/Branches/Customer/5
        [HttpPut("Customer/{id}")]
        public async Task<IActionResult> PutBranchCustomer(int id, AddCustomerDTO addCustomerDTO)
        {
            if (id != addCustomerDTO.Id)
            {
                return BadRequest();
            }

            var branch = await _branchRepository.GetByIdAsync(id);
            var customer = await _customerRepository.GetByIdAsync(addCustomerDTO.CustomerId);

            if(branch.Customers == null)
            {
                branch.Customers = new List<Customer>();
            }

            branch.Customers.Add(customer);
            await _branchRepository.SaveAsync();

            return Ok();
        }
        */
        // POST: api/Branches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Branch>> PostBranch(CreateBranchDTO branchDTO)
        {
            bool created = await _branchService.CreateAsync(branchDTO);
            
            if (!created)
            {
                return Problem("There was a problem creating Branch.");
            }

            await _branchService.SaveAsync();

            return Ok("Branch successfully created.");
        }

        // DELETE: api/Branches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            var branch = await _branchService.GetAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            await _branchService.DeleteAsync(id);
            await _branchService.SaveAsync();

            return NoContent();
        }
    }
}
