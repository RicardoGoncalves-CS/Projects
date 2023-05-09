using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bank.WebAPI.Data;
using Bank.WebAPI.Models;
using Bank.WebAPI.Models.DTOs;

namespace Bank.WebAPI.Controllers;

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
    public async Task<ActionResult<IEnumerable<Branch>>> GetBranches()
    {
        if (_context.Branches == null)
        {
            return NotFound();
        }
        return await _context.Branches.Include(b => b.Accounts).ToListAsync();
    }

    // GET: api/Branches/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Branch>> GetBranch(int id)
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

        return branch;
    }

    // PUT: api/Branches/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBranch(int id, BranchDTO branchDTO)
    {
        //if (id != branch.Id)
        //{
        //    return BadRequest();
        //}

        var branch = await _context.Branches.FindAsync(id);
        if(branch == null)
        {
            return Problem($"Branch with ID {id} was not found.");
        }

        var address = await _context.Addresses.FindAsync(branchDTO.AddressId);
        if (address == null)
        {
            return Problem($"Address with ID {branchDTO.AddressId} was not found.");
        }

        var accounts = new List<Account>();

        foreach(var accountId in branchDTO.AccountIds)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if(account == null)
            {
                return Problem($"Account with ID {accountId} was not found.");
            }

            accounts.Add(account);
        }

        branch.BranchName = branchDTO.BranchName;
        branch.AddressId = branchDTO.AddressId;
        branch.Address = address;
        branch.AccountIds = branchDTO.AccountIds;
        branch.Accounts = accounts;

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
    public async Task<ActionResult<Branch>> PostBranch(BranchDTO branchDTO)
    {
        if (_context.Branches == null)
        {
            return Problem("Entity set 'DataContext.Branches'  is null.");
        }

        var branch = new Branch
        {
            BranchName = branchDTO.BranchName,
            AddressId = branchDTO.AddressId,
            Address = await _context.Addresses.FindAsync(branchDTO.AddressId)
        };

        foreach (var accountId in branchDTO.AccountIds)
        {
            var account = await _context.Accounts.FindAsync(accountId);

            if (accountId == 0)
            {
                return Problem($"Account with ID {branchDTO.AccountIds} not found.");
            }

            branch.Accounts.Add(account);
        }

        _context.Branches.Add(branch);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBranch", new { id = branch.Id }, branch);
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
