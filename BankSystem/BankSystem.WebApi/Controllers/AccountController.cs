using BankSystem.Application.Repositories;
using BankSystem.Application.Validators;
using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
using FluentValidation;
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
        private readonly TransactionValidator _transactionValidator;
        
        public AccountController(TransactionValidator transactionValidator, CreateAccountValidator createValidator, UpdateAccountValidator updateValidator, AccountRepository accountRepository)
        {            
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _accountRepository = accountRepository;
            _transactionValidator = transactionValidator;
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

            await _accountRepository.CreateAccountAsync(accountType, accountModel, this.User);

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

            var balance = await _accountRepository.GetAccountBalanceAsync(accountId, userId);
            if (balance == null)
            {
                return NotFound(new { Message = "Account not found." });
            }

            return Ok(new { Balance = balance });
        }

        [HttpPut("{accountId}/deposit-balance")]
        public async Task<IActionResult> DepositBalance(int accountId, [FromBody] TransactionModel model, decimal changeAmount)
        {
            var result = _transactionValidator.Validate(model);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            await _accountRepository.DepositBalanceAsync(accountId, changeAmount, this.User);

            return Ok(new { Message = "Balance deposited successfully." });
        }

        [HttpPut("{accountId}/withdraw-balance")]
        public async Task<IActionResult> WithdrawBalance(int accountId, [FromBody] AccountModel accountModel, decimal changeAmount)
        {
            var result = _updateValidator.Validate(accountModel);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            await _accountRepository.WithdrawBalanceAsync(accountId, changeAmount, this.User);

            return Ok(new { Message = "Balance withdrawn successfully." });
        }
    }

}
