using BankSystem.Application.Repositories;
using BankSystem.Business.Repositories;
using BankSystem.Data.Enums;
using BankSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.WebApi.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionRepository _transactionRepository;

        public TransactionsController(TransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpPost]
        [Route("withdraw")]
        public async Task<IActionResult> WithdrawBalanceAsync([FromBody] TransactionModel transactionModel, int accountId, decimal changeAmount)
        {
            await _transactionRepository.WithdrawAsync(transactionModel, accountId, changeAmount);

            return Ok(new { Message = "Withdraw created successfully." });
        }

        [HttpPost]
        [Route("deposit")]
        public async Task<IActionResult> DepositAsync([FromBody] TransactionModel transactionModel,int accountId, decimal changeAmount)
        {
            await _transactionRepository.DepositBalanceAsync(transactionModel,accountId, changeAmount);

            return Ok(new { Message = "Deposit created successfully." });
        }

        [HttpPost]
        [Route("transfer/internal")]
        public async Task<IActionResult> InternalTransferAsync(AccountModel accountModel, int accountId, int receiverAccountId, TransactionType transactionType, decimal amount)
        {
            await _transactionRepository.InternalTransferAsync(accountModel, accountId,  receiverAccountId,  transactionType,  amount);

            return Ok(new { Message = "Internal transfer created successfully." });

        }

        [HttpPost]
        [Route("transfer/external")]
        public async Task<IActionResult> ExternalTransferAsync(AccountModel accountModel, int accountId, int receiverAccountId, TransactionType transactionType, decimal amount)
        {
            return Ok();
        }
    }
}
