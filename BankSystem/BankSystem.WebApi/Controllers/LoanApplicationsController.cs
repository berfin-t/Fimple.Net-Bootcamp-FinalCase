using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankSystem.Business.Repositories;
using BankSystem.Domain.Entities;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace BankSystem.WebApi.Controllers
{
    [Route("api/loanApplications")]
    [ApiController]
    public class LoanApplicationsController : ControllerBase
    {
        private readonly LoanApplicationRepository _loanApplicationRepository;

        public LoanApplicationsController(LoanApplicationRepository loanApplicationRepository)
        {
            _loanApplicationRepository = loanApplicationRepository;
        }


        [HttpPost]
        [Route("create-loan")]
        // CreateLoanApplicationAsync
        public async Task<IActionResult> CreateLoanApplicationAsync([FromBody] LoanApplicationModel loanApplicationModel, string loanType, string loanApplicationStatus)
        {
            await _loanApplicationRepository.CreateAsync(loanApplicationModel, loanType, loanApplicationStatus, this.User);

            return Ok(new { Message = "Loan Application created successfully" });
        }

        [HttpPost]
        [Route("id/approve")]
        [Authorize(Roles = "Admin")]
        //ApproveApplicationAsync
        public async Task<IActionResult> ApproveApplicationAsync(int applicationId)
        {
            await _loanApplicationRepository.ApproveApplicationAsync(applicationId);

            return Ok();
        }

        [HttpGet]
        [Route("id/status")]
        // getloanapplicationbystatusasync
        public async Task<IActionResult> GetLoanApplicationByStatusAsync()
        {
            return Ok();
        }

        [HttpGet]
        [Route("id/recommendation")]
        [Authorize(Roles = "Admin,Auditor")]
        //ProcessApplicationAsync
        public async Task<IActionResult> ProcessApplicationAsync()
        {
            return Ok();
        }

        [HttpPut]
        [Route("id/reject")]
        [Authorize(Roles = "Admin")]
        //RejectApplicationAsync
        public async Task<IActionResult> RejectApplicationAsync()
        {
            return Ok();
        }
    }

}

