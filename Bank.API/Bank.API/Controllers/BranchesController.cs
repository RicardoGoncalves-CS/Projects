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
        private readonly IBranchRepository _branchRepository;
        private readonly IBranchService _branchService;

        public BranchesController(IBranchRepository branchRepository, IBranchService branchService)
        {
            _branchRepository = branchRepository;
            _branchService = branchService;
        }

        // GET: api/Branches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Branch>>> GetBranches()
        {
            var branches = await _branchRepository.GetAllAsync();
            if (branches == null)
            {
                return NotFound();
            }

            return Ok(branches);
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
