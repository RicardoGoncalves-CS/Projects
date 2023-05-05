using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bank.API.Data;
using Bank.API.Models;
using Bank.API.Services;
using Bank.API.Models.DTOs.AccountDTOs;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IBankService<CreateAccountDTO, ReadAccountDTO, UpdateAccountDTO> _accountService;

        public AccountsController(IBankService<CreateAccountDTO, ReadAccountDTO, UpdateAccountDTO> accountService)
        {
            _accountService = accountService;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadAccountDTO>>> GetAccounts()
        {
            var accounts = await _accountService.GetAllAsync();
            if (accounts == null)
            {
                return NotFound();
            }

            return Ok(accounts);
        }

        // GET: api/Accounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadAccountDTO>> GetAccount(int id)
        {
            var account = await _accountService.GetAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // PUT: api/Accounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccount(int id, UpdateAccountDTO account)
        {
            if (id != account.Id)
            {
                return BadRequest();
            }

            try
            {
                await _accountService.UpdateAsync(id, account);
                await _accountService.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _accountService.EntityExists(id))
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
        public async Task<ActionResult<Account>> PostAccount(CreateAccountDTO account)
        {
            bool created = await _accountService.CreateAsync(account);

            if (!created)
            {
                return Problem("There was a problem creating Account.");
            }

            await _accountService.SaveAsync();

            return Ok("Account successfully created.");
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _accountService.GetAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            await _accountService.DeleteAsync(id);
            await _accountService.SaveAsync();

            return NoContent();
        }
    }
}
