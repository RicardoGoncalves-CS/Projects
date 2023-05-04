using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bank.API.Data;
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
        private readonly ICustomerRepository _customerRepository;

        public BranchesController(IBankRepository<Branch> branchRepository, IBranchService branchService, ICustomerRepository customerRepository)
        {
            _branchRepository = branchRepository;
            _branchService = branchService;
            _customerRepository = customerRepository;
        }

        // GET: api/Branches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBranchDTO>>> GetBranches()
        {
            var branches = await _branchRepository.GetAllAsync();
            if (branches == null)
            {
                return NotFound();
            }

            var branchDTOs = new List<GetBranchDTO>();

            foreach(var branch in branches)
            {
                var branchDTO = new GetBranchDTO
                {
                    Id = branch.Id,
                    BranchName = branch.BranchName,
                    Address = branch.Address,
                    CustomerIds = branch.Customers?.Select(c => c.Id).ToList()
                };

                branchDTOs.Add(branchDTO);
            }

            return Ok(branchDTOs);
        }

        // GET: api/Branches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetBranchDTO>> GetBranch(int id)
        {
            var branch = await _branchService.GetBranchById(id);
            if (branch == null)
            {
                return NotFound();
            }

            var branchDTO = new GetBranchDTO
            {
                Id = branch.Id,
                BranchName = branch.BranchName,
                Address = branch.Address,
                CustomerIds = branch.CustomerIds
            };

            return Ok(branchDTO);
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
                await _branchService.UpdateBranchAsync(id, branchDTO);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _branchRepository.ExistsAsync(id))
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

        // POST: api/Branches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Branch>> PostBranch(CreateBranchDTO branchDTO)
        {
            var branch = await _branchService.CreateBranchAsync(branchDTO);

            return CreatedAtAction("GetBranch", new { id = branch.Id }, branch);
        }

        // DELETE: api/Branches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            await _branchRepository.RemoveAsync(branch);

            return NoContent();
        }
    }
}
