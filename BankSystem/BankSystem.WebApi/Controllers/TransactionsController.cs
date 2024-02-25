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
        //WithdrawAsync
        public async Task<IActionResult> WithdrawBalanceAsync([FromBody] TransactionModel transactionModel, int accountId, decimal changeAmount)
        {
            await _transactionRepository.WithdrawAsync(transactionModel, accountId, changeAmount);

            return Ok(new { Message = "Withdraw created successfully." });
        }

        //[HttpPost]
        //[Route("deposit")]
        ////DepositAsync
        //public async Task<IActionResult> DepositAsync()
        //{
        //    return Ok();
        //}

        //[HttpPost]
        //[Route("transfer/internal")]
        ////InternalTransferAsync
        //public async Task<IActionResult> InternalTransferAsync()
        //{
        //    return Ok();
        //}

        //[HttpPost]
        //[Route("transfer/external")]
        ////ExternalTransferAsync
        //public async Task<IActionResult> ExternalTransferAsync()
        //{
        //    return Ok();
        //}
    }
}
