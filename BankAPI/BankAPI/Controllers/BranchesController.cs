using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankAPI.Data;
using BankAPI.Models;
using BankAPI.Models.BranchDTOs;
using BankAPI.Services;
using BankAPI.Data.Repository;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        //private readonly DataContext _context;

        //public BranchesController(DataContext context)
        //{
        //    _context = context;
        //}

        private readonly BranchService _branchService;
        private readonly IBankService<Address> _addressService;

        public BranchesController(BranchService branchService, IBankService<Address> addressService)
        {
            _branchService = branchService;
            _addressService = addressService;
        }
        
        // GET: api/Branches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadBranchDTO>>> GetBranches()
        {
            //if (_context.Branches == null)
            //{
            //    return NotFound();
            //}

            //var branches = await _context.Branches
            //    .Include(b => b.Address)
            //    .Include(b => b.Customers)
            //    .Include(b => b.Accounts)
            //    .ToListAsync();

            //var branchesDTO = new List<ReadBranchDTO>();

            //foreach(var branch in branches)
            //{
            //    var branchDTO = new ReadBranchDTO
            //    {
            //        BranchId = branch.BranchId,
            //        BranchName = branch.BranchName,
            //        Address = branch.Address,
            //        CustomerIds = branch.Customers.Select(c => c.CustomerId).ToList(),
            //        AccountIds = branch.Accounts.Select(a => a.AccountId).ToList()
            //    };

            //    branchesDTO.Add(branchDTO);
            //}

            ////return await _context.Branches.ToListAsync();
            //return branchesDTO;

            var branches = (await _branchService.GetAllAsync())
                .ToList();

            var branchesDTO = new List<ReadBranchDTO>();

            foreach (var branch in branches)
            {
                if (branch == null)
                {
                    continue;
                }

                var branchDTO = new ReadBranchDTO
                {
                    BranchId = branch.BranchId,
                    BranchName = branch.BranchName,
                    Address = branch.Address,
                    CustomerIds = branch.Customers?.Select(c => c.CustomerId).ToList() ?? new List<int>(),
                    AccountIds = branch.Accounts?.Select(a => a.AccountId).ToList() ?? new List<int>()
                    //CustomerIds = branch.Customers.Select(c => c.CustomerId).ToList(),
                    //AccountIds = branch.Accounts.Select(a => a.AccountId).ToList()
                };

                branchesDTO.Add(branchDTO);
            }

            return branchesDTO;
            //return (await _branchService.GetAllAsync())
            //    .ToList();
        }

        /*
        // GET: api/Branches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadBranchDTO>> GetBranch(int id)
        {
            if (_context.Branches == null)
            {
                return NotFound();
            }

            var branch = await _context.Branches
                .Where(b => b.BranchId == id)
                .Include(b => b.Address)
                .Include(b => b.Customers)
                .Include(b => b.Accounts)
                .FirstOrDefaultAsync();

            if (branch == null)
            {
                return NotFound();
            }

            var branchDTO = new ReadBranchDTO
            {
                BranchId = branch.BranchId,
                BranchName = branch.BranchName,
                Address = branch.Address,
                CustomerIds = branch.Customers.Select(c => c.CustomerId).ToList(),
                AccountIds = branch.Accounts.Select(a => a.AccountId).ToList()
            };

            return branchDTO;
        }

        // PUT: api/Branches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBranch(int id, UpdateBranchDTO branchDTO)
        {
            if (id != branchDTO.BranchId)
            {
                return BadRequest();
            }

            var address = await _context.Addresses.FindAsync(branchDTO.AddressId);
            if(address == null)
            {
                return Problem($"Address with Id {branchDTO.AddressId} was not found.");
            }

            List<Customer> customers = new List<Customer>();
            List<Account> accounts = new List<Account>();

            foreach(var customerId in branchDTO.CustomerIds)
            {
                customers.Add(await _context.Customers.FindAsync(customerId));
            }

            foreach(var accountId in branchDTO.AccountIds)
            {
                accounts.Add(await _context.Accounts.FindAsync(accountId));
            }

            var branch = new Branch
            {
                BranchId = branchDTO.BranchId,
                BranchName = branchDTO.BranchName,
                Address = address,
                Customers = customers,
                Accounts = accounts
            };

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
        */
        // POST: api/Branches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Branch>> PostBranch(
            [Bind("BranchId", "BranchName", "AddressId")]CreateBranchDTO branchDTO)
        {
            //if (_context.Branches == null)
            //{
            //    return Problem("Entity set 'DataContext.Branches'  is null.");
            //}

            //Address address = await _context.Addresses.FindAsync(branchDTO.AddressId);

            //if(address == null)
            //{
            //    return Problem($"Address with Id {branchDTO.AddressId} not found.");
            //}

            //Branch branch = new Branch
            //{
            //    BranchName = branchDTO.BranchName,
            //    Address = address,
            //    Customers = new List<Customer>(),
            //    Accounts = new List<Account>()
            //};

            //_context.Branches.Add(branch);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetBranch", new { id = branch.BranchId }, branch);

            var address = await _addressService.GetAsync(branchDTO.AddressId);
            if(address == null)
            {
                return Problem($"Address with Id {branchDTO.AddressId} was not found.");
            }

            var branch = new Branch
            {
                BranchName = branchDTO.BranchName,
                Address = address,
                Customers = new List<Customer>(),
                Accounts = new List<Account>()
            };

            bool created = await _branchService.CreateAsync(branch);

            if (created == false)
            {
                return Problem("There was a problem creating Branch.");
            }

            //return CreatedAtAction("GetBranch", new { id = branch.BranchId }, branch);
            return Ok();
        }
        /*
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
            return (_context.Branches?.Any(e => e.BranchId == id)).GetValueOrDefault();
        }
        */
    }
}
