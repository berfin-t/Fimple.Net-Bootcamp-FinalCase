using AutoMapper;
using BankSystem.Application.Dto;
using BankSystem.Data.Enums;
using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
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
                //var userId = UserIdClaimControl(user);

                var account = _context.Account.FirstOrDefault(a => a.AccountId == accountId);
                //if (account != null)
                //{
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
                var limit = transactionType == TransactionType.InternalTransfer ? LimitPerInternalTransfer : LimitPerExternalTransfer;
                var dailyLimit = transactionType == TransactionType.InternalTransfer ? DailyInternalTransferLimit : DailyExternalTransferLimit;

                if (amount > limit)
                    throw new InvalidOperationException($"This operation exceeds the transfer limit of ${limit} per transfer.");
                if (amount > dailyLimit)
                    throw new InvalidOperationException($"This operation exceeds the daily transfer limit of {dailyLimit}.");

                var account = _context.Account.FirstOrDefault(a => a.AccountId == accountId);

                //var senderAccount = account.AccountId;
                //var receiverAccount = account.ReceiverAccountId;

                var internalTransfer = new TransactionModel
                {
                    AccountId = accountId,
                    Amount = amount,
                    ReceiverAccountId = receiverAccountId,
                    TransactionType = TransactionType.InternalTransfer
                };

                _context.Transaction.Add(internalTransfer);
                account.Balance += amount;
                _context.SaveChanges();
            }
            catch(Exception)
            {
                transaction.Rollback();
                throw;

            }
            

        }
    }
}

