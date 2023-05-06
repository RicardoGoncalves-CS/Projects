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
        private readonly IBranchService _branchService;
        private readonly IBankRepository<Customer> _customerRepository;

        public BranchesController(IBankRepository<Branch> branchRepository, IBranchService branchService, IBankRepository<Customer> customerRepository)
        {
            _branchRepository = branchRepository;
            _branchService = branchService;
            _customerRepository = customerRepository;
        }

        // GET: api/Branches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadBranchDTO>>> GetBranches()
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
        public async Task<ActionResult<ReadBranchDTO>> GetBranch(int id)
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
        
        // PUT: api/Branches/Customer/5
        [HttpPut("Customer/{id}")]
        public async Task<IActionResult> PutBranchCustomer(int id, AddCustomerBranchDTO addCustomerDTO)
        {
            if (id != addCustomerDTO.Id)
            {
                return BadRequest();
            }

            var added = await _branchService.AddCustomerToBranchAsync(id, addCustomerDTO.CustomerId);
            if (!added)
            {
                return Problem($"There was a problem adding Customer with Id {addCustomerDTO.CustomerId} to Branch with Id {id}.");
            }


            await _branchService.SaveAsync();

            return Ok();
        }

        // PUT: api/Branches/Customer/5
        [HttpPut("Account/{id}")]
        public async Task<IActionResult> PutBranchAccount(int id, AddCustomerBranchDTO addCustomerDTO)
        {
            if (id != addCustomerDTO.Id)
            {
                return BadRequest();
            }

            var added = await _branchService.AddAccountToBranchAsync(id, addCustomerDTO.CustomerId);
            if (!added)
            {
                return Problem($"There was a problem adding Account with Id {addCustomerDTO.CustomerId} to Branch with Id {id}.");
            }

            await _branchService.SaveAsync();

            return Ok();
        }

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
