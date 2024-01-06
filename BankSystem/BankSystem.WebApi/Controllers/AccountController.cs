using BankSystem.Application.Validators;
using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankSystem.WebApi.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly BankingDbContext _context;
        private readonly CreateAccountValidator _createValidator;
        private readonly UpdateAccountValidator _updateValidator;

        public AccountController(BankingDbContext context, CreateAccountValidator createValidator, UpdateAccountValidator updateValidator)
        {
            _context = context;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpPost("create-account")]
        public IActionResult CreateAccount(string accountType, [FromBody] AccountModel accountModel)
        {
            var result = _createValidator.Validate(accountModel);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var userIdClaim = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
            int userIdToken = int.Parse(userIdClaim.Value);
            if (userIdToken == null)
            {
                return Forbid();
            }

            var newAccount = new AccountModel
            {
                Balance = accountModel.Balance,
                AccountType = accountType,
                CreatedAt = DateTime.Now,
                UserId = userIdToken 
            };

            _context.Account.Add(newAccount);
            _context.SaveChanges();

            return Ok(new { Message = "Account created successfully." });
        }

        [HttpGet("{accountId}/balance")]
        public IActionResult GetAccountBalance(int accountId)
        {
            var userIdClaim = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Forbid();
            }

            int userId = int.Parse(userIdClaim.Value);


            var account = _context.Account.FirstOrDefault(a => a.AccountId == accountId &&  a.UserId == userId);
            if (account == null)
            {
                return NotFound(new { Message = "Account not found." });
            }

            return Ok(new { Balance = account.Balance });
        
        }

        [HttpPut("{accountId}/deposit-balance")]
        public IActionResult DepositBalance(int accountId, [FromBody] AccountModel model, decimal changeAmount)
        {
            var userIdClaim = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
            int userIdToken = int.Parse(userIdClaim.Value);
            if (userIdToken == null)
            {
                return Forbid();
            }

            model.Balance += changeAmount;
            _context.SaveChanges();

            return Ok(new { Message = "Balance updated successfully." });
        }

        [HttpPut("{accountId}/withdraw-balance")]
        public IActionResult WithdrawBalance(int accountId, [FromBody] AccountModel model, decimal changeAmount)
        {
            var result = _updateValidator.Validate(model);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var userIdClaim = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
            int userIdToken = int.Parse(userIdClaim.Value);
            if (userIdToken == null)
            {
                return Forbid();
            }

            model.Balance -= changeAmount;
            _context.SaveChanges();

            return Ok(new { Message = "Balance updated successfully." });
        }
    }

}
