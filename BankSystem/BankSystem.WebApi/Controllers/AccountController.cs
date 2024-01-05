using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.WebApi.Controllers
{
   // [Authorize(Roles = "admin,accountManager,user")]
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly BankingDbContext _context;

        public AccountController(BankingDbContext context)
        {
            _context = context;
        }
        [HttpPost("{userId}/create-account")]
        public IActionResult CreateAccount(string userId, [FromBody] AccountModel accountModel)
        {
            var currentUserIdClaim = User.FindFirst("UserId");
            //if (currentUserIdClaim == null || currentUserIdClaim.Value != userId)
            //{
            //    return Forbid(); 
            //}

            var newAccount = new AccountModel
            {
                Balance = accountModel.Balance,
                AccountHolderName = accountModel.AccountHolderName,
                AccountType = accountModel.AccountType,
                CreatedAt = DateTime.Now,
                UserId = userId 
            };

            _context.Account.Add(newAccount);
            _context.SaveChanges();

            return Ok(new { Message = "Account created successfully." });
        }

        [HttpGet("{accountId}/balance")]
        public IActionResult GetAccountBalance(int accountId, AccountModel account)
        {
            
            var accountIdClaim = User.FindFirst("AccountId");
            if (accountIdClaim == null)
            {
                return Forbid(); 
            }

            int userId = int.Parse(accountIdClaim.Value);

            
            if (accountId != userId)
            {
                return Forbid(); 
            }


            return Ok(new { Balance = account.Balance });
        }

        [HttpPut("{accountId}/update-balance")]
        public IActionResult UpdateAccountBalance(int accountId, [FromBody] TransactionModel transactionModel)
        {
            var accountIdClaim = User.FindFirst("AccountId");
            if (accountIdClaim == null)
            {
                return Forbid(); 
            }

            int userId = int.Parse(accountIdClaim.Value);

            if (accountId != userId)
            {
                return Forbid(); 
            }

            return Ok(new { Message = "Balance updated successfully." });
        }
    }


}
