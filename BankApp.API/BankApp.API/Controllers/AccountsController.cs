using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankApp.API.Data;
using BankApp.API.Models;
using BankApp.API.Models.DTOs.AccountDTOs;

namespace BankApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly DataContext _context;

        public AccountsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetAccountDTO>>> GetAccounts()
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            var accounts = await _context.Accounts
                .Include(a => a.Customer)
                .Include(a => a.Branch)
                .Include(a => a.Transactions)
                .ToListAsync();

            var accountsDTO = new List<GetAccountDTO>();

            foreach (var account in accounts)
            {
                accountsDTO.Add(Utils.GetAccountToDTO(account));
            }

            return accountsDTO;
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetAccountDTO>> GetAccount(int id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Where(a => a.Id == id)
                .Include(a => a.Customer.Id)
                .Include(a => a.Branch.Id)
                .Include(a => a.Transactions)
                .FirstOrDefaultAsync();

            if (account == null)
            {
                return NotFound();
            }

            return Utils.GetAccountToDTO(account);
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, AccountDTO accountDTO)
        {
            var account = await _context.Accounts.FindAsync(id);
            if(account == null)
            {
                return NotFound($"Account with Id {accountDTO.Id} doesn't exist.");
            }

            if (id != accountDTO.Id)
            {
                return BadRequest();
            }

            account.Balance = accountDTO.Balance;
            account.IsActive = accountDTO.IsActive;

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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

        // POST: api/Accounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(
            [Bind("AccountNo", "Balance", "OpenDate", "IsActive", "CustomerId", "BranchId")]AccountDTO accountDTO)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'DataContext.Accounts'  is null.");
            }

            var customer = await _context.Customers.FindAsync(accountDTO.CustomerId);

            if(customer == null)
            {
                return Problem($"Customer with Id {accountDTO.CustomerId} doesn't exist.");
            }

            var branch = await _context.Branches.FindAsync(accountDTO.BranchId);

            if (customer == null)
            {
                return Problem($"Branch with Id {accountDTO.BranchId} doesn't exist.");
            }

            Account account = new Account
            {
                AccountNo = accountDTO.AccountNo,
                Balance = accountDTO.Balance,
                OpenDate = accountDTO.OpenDate,
                IsActive = accountDTO.IsActive,
                Customer = customer,
                Branch = branch,
                //Transactions = new List<Transaction>()
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccount", new { id = account.Id }, account);
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            if (_context.Accounts == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
