using AutoMapper;
using BankSystem.Application.Dto;
using BankSystem.Application.Repositories;
using BankSystem.Application.Validators;
using BankSystem.Domain.Entities;
using BankSystem.Persistence.Context;
using DocumentFormat.OpenXml.Spreadsheet;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankSystem.WebApi.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {   
       
        private readonly AccountRepository _accountRepository;
        private readonly CreateAccountValidator _createAccountValidator;
        private readonly IMapper _mapper;
        
        public AccountsController(AccountRepository accountRepository, CreateAccountValidator createAccountValidator)
        {
            _accountRepository = accountRepository;
            _createAccountValidator = createAccountValidator;
        }

        [HttpPost("create-account")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAccountAsync(string accountType, [FromBody] AccountModel accountModel)
        {            
            var result = _createAccountValidator.Validate(accountModel);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            await _accountRepository.CreateAccountAsync(accountType, accountModel, this.User);

            return Ok(new { Message = "Account created successfully." });
        }

        [HttpGet("id/balance")]
        public async Task<IActionResult> GetAccountBalanceByIdAsync(int accountId)
        {
            var balance = await _accountRepository.GetAccountBalanceAsync(accountId, this.User);
            if (balance == null)
            {
                return NotFound(new { Message = "Account not found." });
            }

            return Ok(new { Balance = balance });
        }

        [HttpPut]
        [Route("id/update-balance")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAccountBalanceAsync([FromBody] AccountModel model, int accountId)
        {
            var account = _accountRepository.GetAccountById(accountId);
            if (account == null)
            {
                return NotFound(new { Message = "Account not found." });
            }

            _accountRepository.UpdateBalanceAsync(accountId, model.Balance);

            return Ok(new { Message = "The account balance updated successfully." });
        }

        //[HttpPut]
        //[Route("{accountId:guid}/loan-payment")]

        //[HttpPut("{accountId}/deposit-balance")]
        //public async Task<IActionResult> DepositBalance(int accountId, [FromBody] TransactionModel model, decimal changeAmount)
        //{
        //    var result = _transactionValidator.Validate(model);
        //    if (!result.IsValid)
        //    {
        //        return BadRequest(result.Errors);
        //    }

        //    await _accountRepository.DepositBalanceAsync(accountId, changeAmount, this.User);

        //    return Ok(new { Message = "Balance deposited successfully." });
        //}
        
    }

}
