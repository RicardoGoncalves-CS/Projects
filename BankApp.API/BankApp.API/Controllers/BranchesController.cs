using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankApp.API.Data;
using BankApp.API.Models;
using BankApp.API.Models.DTOs;

namespace BankApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly DataContext _context;

        public BranchesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Branches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetBranchDTO>>> GetBranches()
        {
            if (_context.Branches == null)
            {
                return NotFound();
            }
            var branches = await _context.Branches
                .Include(b => b.Address)
                .Include(b => b.Accounts)
                .Include(b => b.Customers)
                .ToListAsync();

            var branchesDTO = new List<GetBranchDTO>();

            foreach(var branch in branches)
            {
                branchesDTO.Add(Utils.GetBranchToDTO(branch));
            }

            return branchesDTO;
        }

        // GET: api/Branches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetBranchDTO>> GetBranch(int id)
        {
            if (_context.Branches == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches
                .Where(b => b.Id == id)
                .Include(b => b.Address)
                .Include(b => b.Accounts)
                .Include(b => b.Customers)
                .FirstOrDefaultAsync();

            if (branch == null)
            {
                return NotFound();
            }

            return Utils.GetBranchToDTO(branch);
        }

        // PUT: api/Branches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBranch(
            [Bind("Id", "Name", "AddressId", "Accounts", "Customers")]int id, PutBranchDTO branch)
        {
            if (id != branch.Id)
            {
                return BadRequest();
            }

            _context.Entry(branch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
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
        public async Task<ActionResult<Branch>> PostBranch(PostBranchDTO branchDTO)
        {
            if (_context.Branches == null)
            {
                return Problem("Entity set 'DataContext.Branches'  is null.");
            }

            var address = await _context.Addresses.FindAsync(branchDTO.AddressId);
            if(address == null)
            {
                return Problem($"Address with Id:{branchDTO.AddressId} can't be found");
            }

            var branch = new Branch
            {
                Id = branchDTO.Id,
                Name = branchDTO.Name,
                Address = address,
                Accounts = new List<Account>(),
                Customers = new List<Customer>()
            };

            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();

            return Ok("Branch successfully created");
        }

        // DELETE: api/Branches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            if (_context.Branches == null)
            {
                return NotFound();
            }
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BranchExists(int id)
        {
            return (_context.Branches?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
