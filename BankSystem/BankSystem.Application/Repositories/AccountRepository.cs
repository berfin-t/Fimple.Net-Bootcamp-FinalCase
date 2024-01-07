using BankSystem.Persistence.Context;
using BankSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

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

        public async Task CreateAccountRepo(string accountType, [FromBody] AccountModel accountModel, ClaimsPrincipal user)
        {
            var userId = UserIdClaimControl(user);

            var newAccount = _mapper.Map<AccountModel>(accountModel);
            newAccount.AccountType = accountType;
            newAccount.CreatedAt = DateTime.Now;
            newAccount.UserId = userId;

            _context.Account.Add(newAccount);
            await _context.SaveChangesAsync();
        }

        public async Task DepositBalanceRepo(int accountId, decimal changeAmount, ClaimsPrincipal user)
        {
            var userId = UserIdClaimControl(user);

            var account = await _context.Account.FirstOrDefaultAsync(a => a.AccountId == accountId && a.UserId == userId);
            if (account != null)
            {
                account.Balance += changeAmount;
                await _context.SaveChangesAsync();
            }
        }

        public async Task WithdrawBalanceRepo(int accountId, [FromBody] AccountModel model, decimal changeAmount, ClaimsPrincipal user)
        {
            var userId = UserIdClaimControl(user);

            var account = await _context.Account.FirstOrDefaultAsync(a => a.AccountId == accountId && a.UserId == userId);
            if (account != null)
            {
                account.Balance -= changeAmount;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal?> GetAccountBalanceRepo(int accountId, int userId)
        {
            var account = await _context.Account
                .FirstOrDefaultAsync(a => a.AccountId == accountId && a.UserId == userId);

            return account?.Balance;
        }
    }
}
