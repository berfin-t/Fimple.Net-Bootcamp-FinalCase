using BankSystem.Persistence.Context;
using BankSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BankSystem.Data.Enums;

namespace BankSystem.Application.Repositories
{
    public class AccountRepository
    {
        private readonly BankingDbContext _context;
        private readonly IMapper _mapper;
        public AccountRepository(IMapper mapper,BankingDbContext context) 
        { 
            _context = context;
            _mapper = mapper;
        }

        private int UserIdClaimControl(ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }
        public async Task CreateAccountAsync(string accountType, [FromBody] AccountModel accountModel, ClaimsPrincipal user)
        {
            var userIdClaim = UserIdClaimControl(user);

            if (!Enum.TryParse<AccountType>(accountType, out var accountTypeEnum))
            {
                throw new ArgumentException("Invalid account type", nameof(accountType));
            }
            var newAccount = _mapper.Map<AccountModel>(accountModel);

            newAccount.AccountType = accountTypeEnum;
            newAccount.CreatedAt = DateTime.Now;
            newAccount.UserId = userIdClaim;

            _context.Account.Add(newAccount);
            await _context.SaveChangesAsync();
        }        

        public async Task<decimal?> GetAccountBalanceAsync(int accountId, ClaimsPrincipal user)
        {
            var userIdClaim = UserIdClaimControl(user);            

            var account = await _context.Account
                .FirstOrDefaultAsync(a => a.AccountId == accountId);

            return account?.Balance;
        }

        public async Task<AccountModel?> GetAccountById(int accountId)
        {
            return _context.Account.Find(accountId);
        }

        public async Task UpdateBalanceAsync(int accountId, decimal balance)
        {          
            var account = _context.Account.Find(accountId);
            if (account != null)
            {
                account.Balance = balance;
                _context.SaveChanges();
            }
        }

        public async Task LoanPaymentAsync(int accountId, decimal amount)
        {
            var account = _context.Account.Find(accountId);
            //var loan = _context.Loan.Find(loanId);
            if (account != null)
            {
                account.Balance -= amount;
                //loan.LoanAmount += amount;
                _context.SaveChanges();
            }
        }
    }
}
