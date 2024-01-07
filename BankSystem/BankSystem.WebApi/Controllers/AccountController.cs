using BankSystem.Application.Repositories;
using BankSystem.Application.Validators;
using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankSystem.WebApi.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly CreateAccountValidator _createValidator;
        private readonly UpdateAccountValidator _updateValidator;
        private readonly AccountRepository _accountRepository;

        public AccountController(CreateAccountValidator createValidator, UpdateAccountValidator updateValidator, AccountRepository accountRepository)
        {            
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _accountRepository = accountRepository;
        }

        [HttpPost("create-account")]
        public async Task<IActionResult> CreateAccount(string accountType, [FromBody] AccountModel accountModel)
        {
            var result = _createValidator.Validate(accountModel);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var userIdClaim = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Forbid();
            }

            await _accountRepository.CreateAccountRepo(accountType, accountModel, this.User);

            return Ok(new { Message = "Account created successfully." });
        }

        [HttpGet("{accountId}/balance")]
        public async Task<IActionResult> GetAccountBalance(int accountId)
        {
            var userIdClaim = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Forbid();
            }

            var balance = await _accountRepository.GetAccountBalanceRepo(accountId, userId);
            if (balance == null)
            {
                return NotFound(new { Message = "Account not found." });
            }

            return Ok(new { Balance = balance });
        }

        [HttpPut("{accountId}/deposit-balance")]
        public async Task<IActionResult> DepositBalance(int accountId, [FromBody] AccountModel model, decimal changeAmount)
        {
            await _accountRepository.DepositBalanceRepo(accountId, changeAmount, this.User);

            return Ok(new { Message = "Balance deposited successfully." });
        }

        [HttpPut("{accountId}/withdraw-balance")]
        public async Task<IActionResult> WithdrawBalance(int accountId, [FromBody] AccountModel model, decimal changeAmount)
        {
            var result = _updateValidator.Validate(model);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            await _accountRepository.WithdrawBalanceRepo(accountId, model, changeAmount, this.User);

            return Ok(new { Message = "Balance withdrawn successfully." });
        }
    }

}
