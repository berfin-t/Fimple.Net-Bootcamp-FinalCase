using AutoMapper;
using BankSystem.Application.Dto;
using BankSystem.Data.Enums;
using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Business.Repositories
{
    public class TransactionRepository
    {
        private readonly BankingDbContext _context;
        private readonly IMapper _mapper;

        public TransactionRepository(BankingDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task WithdrawAsync(TransactionModel transactionModel, int accountId, decimal changeAmount)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                //var userId = UserIdClaimControl(user);

                //var account = await _context.Account.FirstOrDefaultAsync(a => a.AccountId == accountId && a.UserId == userId);
                //if (account != null)
                //{
                var account = _context.Account.FirstOrDefault(a => a.AccountId == accountId);
                if (account.Balance >= changeAmount)
                    {
                    // Log withdrawal transaction
                    var withdrawalTransaction = new TransactionModel
                    {
                        AccountId = accountId,
                        Amount = changeAmount,
                        //TransactionDate 
                        //TransactionType = "Withdraw",
                        TransactionType = TransactionType.Withdraw
                        };
                        _context.Transaction.Add(withdrawalTransaction);
                        account.Balance -= changeAmount;
                        await _context.SaveChangesAsync();
                    }
                    transaction.Commit();
                
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
