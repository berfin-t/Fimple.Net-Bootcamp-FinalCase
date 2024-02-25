using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.WebApi.Controllers
{
    [Route("api/supportTickets")]
    [ApiController]
    public class SupportTicketsController : ControllerBase
    {
        //[HttpGet]
        //[Route("{id:guid}")]
        ////GetByIdAsync
        //public async Task<IActionResult> GetByIdAsync()
        //{
        //    return Ok();
        //}

        //[HttpGet]
        //[Route("user/{userId}")]
        ////GetAllUserIdAsync
        //public async Task<IActionResult> GetAllUserIdAsync()
        //{
        //    return Ok();
        //}

        //[HttpGet]
        //[Authorize(Roles = "Admin,Auditor")]
        ////GetAllAsync
        //public async Task<IActionResult> GetAllAsync()
        //{
        //    return Ok();
        //}

        //[HttpPut]
        //[Route("id/status")]
        //[Authorize(Roles = "Admin")]
        ////UpdateTicketStatusAsync
        //public async Task<IActionResult> UpdateTicketStatusAsync()
        //{
        //    return Ok();
        //}

        //[HttpPut]
        //[Route("id/priority")]
        //[Authorize(Roles = "Admin")]
        ////UpdateTicketPriorityAsync
        //public async Task<IActionResult> UpdateTicketPriorityAsync()
        //{
        //    return Ok();
        //}
    }
}
