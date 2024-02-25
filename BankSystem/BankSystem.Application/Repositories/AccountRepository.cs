using BankSystem.Persistence.Context;
using BankSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DocumentFormat.OpenXml.InkML;
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
            var userId = UserIdClaimControl(user);

            if (!Enum.TryParse<AccountType>(accountType, out var accountTypeEnum))
            {
                throw new ArgumentException("Invalid account type", nameof(accountType));
            }

            var newAccount = _mapper.Map<AccountModel>(accountModel);
            newAccount.AccountType = accountTypeEnum;
            newAccount.CreatedAt = DateTime.Now;
            newAccount.UserId = userId;

            _context.Account.Add(newAccount);
            await _context.SaveChangesAsync();
        }

        //public async Task DepositBalanceAsync(int accountId, decimal changeAmount, ClaimsPrincipal user)
        //{
        //    using var transaction = _context.Database.BeginTransaction();

        //    try
        //    {
        //        var userId = UserIdClaimControl(user);

        //        var account = await _context.Account.FirstOrDefaultAsync(a => a.AccountId == accountId && a.UserId == userId);
        //        if (account != null)
        //        {
        //            var depositTransaction = new TransactionModel
        //            {
        //                AccountId = accountId,
        //                Amount = changeAmount,
        //                TransactionType = "Deposit"
        //            };

        //            _context.Transaction.Add(depositTransaction);
        //            account.Balance += changeAmount;
        //            await _context.SaveChangesAsync();
        //        }
        //        transaction.Commit();
        //    }
        //    catch (Exception)
        //    {
        //        transaction.Rollback();
        //        throw;
        //    }

        //}

        //public async Task WithdrawBalanceAsync(int accountId, decimal changeAmount, ClaimsPrincipal user)
        //{
        //    using var transaction = _context.Database.BeginTransaction();

        //    try
        //    {
        //        var userId = UserIdClaimControl(user);

        //        var account = await _context.Account.FirstOrDefaultAsync(a => a.AccountId == accountId && a.UserId == userId);
        //        if (account != null)
        //        {
        //            if (account.Balance >= changeAmount)
        //            {
        //                // Log withdrawal transaction
        //                var withdrawalTransaction = new TransactionModel
        //                {
        //                    AccountId = accountId,
        //                    Amount = changeAmount,
        //                    TransactionType = "Withdrawal"
        //                };
        //                _context.Transaction.Add(withdrawalTransaction);
        //                account.Balance -= changeAmount;
        //                await _context.SaveChangesAsync();
        //            }
        //            transaction.Commit();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        transaction.Rollback();
        //        throw;
        //    }
        //}

        public async Task<decimal?> GetAccountBalanceAsync(int accountId, int userId)
        {
            var account = await _context.Account
                .FirstOrDefaultAsync(a => a.AccountId == accountId && a.UserId == userId);

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
    }
}
