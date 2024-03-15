using AutoMapper;
using BankSystem.Application.Dto;
using BankSystem.Data.Enums;
using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Business.Repositories
{
    public class TransactionRepository
    {

        public const decimal LimitPerInternalTransfer = 10000;
        public const decimal LimitPerExternalTransfer = 8000;
        public const decimal DailyInternalTransferLimit = 30000;
        public const decimal DailyExternalTransferLimit = 24000;
        public const decimal LimitPerDepositAndWithdraw = 20000;

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
                var account = _context.Account.FirstOrDefault(a => a.AccountId == accountId);
                if (account.Balance >= changeAmount)
                {
                    var withdrawalTransaction = new TransactionModel
                    {
                        AccountId = accountId,
                        Amount = changeAmount,
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
        public async Task DepositBalanceAsync(TransactionModel transactionModel, int accountId, decimal changeAmount)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var account = _context.Account.FirstOrDefault(a => a.AccountId == accountId);

                var depositTransaction = new TransactionModel
                {
                    AccountId = accountId,
                    Amount = changeAmount,
                    TransactionType = TransactionType.Deposit
                };

                _context.Transaction.Add(depositTransaction);
                account.Balance += changeAmount;
                await _context.SaveChangesAsync();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task InternalTransferAsync(AccountModel accountModel, int accountId, int receiverAccountId,  TransactionType transactionType,decimal amount)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var senderAccount = await _context.Account.FirstOrDefaultAsync(a => a.AccountId == accountId);
                var receiverAccount = await _context.Account.FirstOrDefaultAsync(a => a.AccountId == receiverAccountId);

                if (senderAccount == null || receiverAccount == null)
                    throw new InvalidOperationException("One or both accounts not found.");

                if (senderAccount.Balance < amount)
                    throw new InvalidOperationException("Insufficient balance.");

                var limit = LimitPerInternalTransfer;
                var dailyLimit = DailyInternalTransferLimit;

                if (amount > limit)
                    throw new InvalidOperationException($"This operation exceeds the transfer limit of ${limit} per transfer.");

                if (amount > dailyLimit)
                    throw new InvalidOperationException($"This operation exceeds the daily transfer limit of {dailyLimit}.");


                var internalTransfer = new TransactionModel
                {
                    AccountId = accountId,
                    ReceiverAccountId = receiverAccountId,
                    Amount = amount,
                    TransactionType = TransactionType.InternalTransfer
                };

                _context.Transaction.Add(internalTransfer);
                senderAccount.Balance -= amount;
                receiverAccount.Balance += amount;

                await _context.SaveChangesAsync();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }

        }

        public async Task ExternalTransferAsync(AccountModel accountModel, int accountId, int receiverAccountId, TransactionType transactionType, decimal amount)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var senderAccount = await _context.Account.FirstOrDefaultAsync(a => a.AccountId == accountId);

                if (senderAccount == null)
                    throw new InvalidOperationException("Sender account not found.");

                if (senderAccount.Balance < amount)
                    throw new InvalidOperationException("Insufficient balance.");

                // Assuming there is a method to retrieve receiver account details based on the account number
                var receiverAccount = await _context.Account.FirstOrDefaultAsync(a => a.AccountId == receiverAccountId);

                if (receiverAccount == null)
                    throw new InvalidOperationException("Receiver account not found.");

                // Check external transfer limits
                var limit = LimitPerExternalTransfer;
                var dailyLimit = DailyExternalTransferLimit;

                if (amount > limit)
                    throw new InvalidOperationException($"This operation exceeds the transfer limit of ${limit} per transfer.");

                if (amount > dailyLimit)
                    throw new InvalidOperationException($"This operation exceeds the daily transfer limit of {dailyLimit}.");

                // Perform the external transfer
                var externalTransfer = new TransactionModel
                {
                    AccountId = accountId,
                    ReceiverAccountId = receiverAccountId,
                    Amount = amount,
                    TransactionType = TransactionType.ExternalTransfer
                };

                _context.Transaction.Add(externalTransfer);
                senderAccount.Balance -= amount;

                await _context.SaveChangesAsync();
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

