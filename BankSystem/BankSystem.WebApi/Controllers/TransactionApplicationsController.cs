using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.WebApi.Controllers
{
    [Route("api/transactionApplications")]
    [ApiController]
    public class TransactionApplicationsController : ControllerBase
    {
        //[HttpPost]
        //[Route("id/approve")]
        //[Authorize(Roles = "Admin")]
        ////ApproveApplication
        //public async Task<IActionResult> ApproveApplication()
        //{
        //    return Ok();
        //}

        //[HttpPost]
        //[Route("id/reject")]
        //[Authorize(Roles = "Admin")]
        ////RejectApplication
        //public async Task<IActionResult> RejectApplication()
        //{
        //    return Ok();
        //}
    }
}
