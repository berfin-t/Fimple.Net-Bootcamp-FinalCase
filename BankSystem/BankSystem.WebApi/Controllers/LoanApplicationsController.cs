using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.WebApi.Controllers
{
    [Route("api/loanApplications")]
    [ApiController]
    public class LoanApplicationsController : ControllerBase
    {
        //[HttpPost]
        //// CreateLoanApplicationAsync
        //public async Task<IActionResult> CreateLoanApplicationAsync()
        //{
        //    return Ok();
        //}

        //[HttpPost]
        //[Route("id/approve")]
        //[Authorize(Roles = "Admin")]
        ////ApproveApplicationAsync
        //public async Task<IActionResult> ApproveApplicationAsync()
        //{
        //    return Ok();
        //}

        //[HttpGet]
        //[Route("id}/status")]
        //// GetLoanApplicationByStatusAsync
        //public async Task<IActionResult> GetLoanApplicationByStatusAsync()
        //{
        //    return Ok();
        //}

        //[HttpGet]
        //[Route("id/recommendation")]
        //[Authorize(Roles = "Admin,Auditor")]
        ////ProcessApplicationAsync
        //public async Task<IActionResult> ProcessApplicationAsync()
        //{
        //    return Ok();
        //}

        //[HttpPut]
        //[Route("id/reject")]
        //[Authorize(Roles = "Admin")]
        ////RejectApplicationAsync
        //public async Task<IActionResult> RejectApplicationAsync()
        //{
        //    return Ok();
        //}
    }

}

