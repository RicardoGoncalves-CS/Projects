﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankWebAPI.Data;
using BankWebAPI.Models;
using BankWebAPI.Models.DTOs;

namespace BankWebAPI.Controllers;

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
    public async Task<ActionResult<IEnumerable<BranchDTO>>> GetBranches()
    {
        if (_context.Branches == null)
        {
            return NotFound();
        }

        var branches = await _context.Branches.ToListAsync();
        var branchDTOs = new List<BranchDTO>();

        foreach(var branch in branches)
        {
            var branchDTO = new BranchDTO
            {
                BranchName = branch.BranchName,
                AddressId = branch.AddressId,
                AccountIds = branch.Accounts?.Select(a => a.Id).ToList()
            };

            branchDTOs.Add(branchDTO);
        }

        return branchDTOs;
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

        var branchDTO = new BranchDTO
        {
            BranchName = branch.BranchName,
            AddressId = branch.AddressId,
            AccountIds = branch.Accounts?.Select(a => a.Id).ToList()
        };

        return branch;
    }

    // PUT: api/Branches/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBranch(int id, BranchDTO branchDTO)
    {
        var branch = await _context.Branches.FindAsync(id);

        if(branch == null)
        {
            return BadRequest($"Branch with ID {id} was not found.");
        }

        //if (id != branch.Id)
        //{
        //    return BadRequest();
        //}

        branch.BranchName = branchDTO.BranchName;
        branch.AddressId = branchDTO.AddressId;
        branch.Address = await _context.Addresses.FindAsync(branchDTO.AddressId);
        
        foreach(var accountId in branchDTO.AccountIds)
        {
            var account = await _context.Accounts.FindAsync(accountId);

            if (accountId == null)
            {
                return Problem($"Account with ID {branchDTO.AccountIds} not found.");
            }

            branch.Accounts.Add(account);
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
    public async Task<ActionResult<Branch>> PostBranch(BranchDTO branchDTO)
    {
        if (_context.Branches == null)
        {
            return Problem("Entity set 'DataContext.Branches' is null.");
        }

        Branch branch = new Branch
        {
            BranchName = branchDTO.BranchName,
            AddressId = branchDTO.AddressId,
            Address = await _context.Addresses.FindAsync(branchDTO.AddressId),
        };

        foreach (var accountId in branchDTO.AccountIds)
        {
            var account = await _context.Accounts.FindAsync(accountId);

            if(accountId == 0)
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
