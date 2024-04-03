using BankSystem.Business.Repositories;
using BankSystem.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.WebApi.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentRepository _paymentRepository;

        public PaymentsController(PaymentRepository paymentRepository) 
        { 
            _paymentRepository = paymentRepository;
        }

        [HttpPost]
        [Route("create-payment")]
        public async Task<IActionResult> CreatePaymentAsync([FromBody] PaymentModel paymentModel, string timePeriod)
        {
            await _paymentRepository.CreateAsync(paymentModel, timePeriod);
            
            return Ok(new { Message = "Payment created successfully" });

        }

        [HttpPut]
        [Route("id/update")]
        //UpdateAsync
        public async Task<IActionResult> UpdatePaymentAsync([FromBody] PaymentModel model, int paymentId)
        {
            _paymentRepository.UpdateAsync(paymentId, model.Amount);

            return Ok(new { Message = "The payment amount updated successfully." });
        }

        //[HttpDelete]
        //[Route("id/delete")]
        ////DeleteAsync
        //public async Task<IActionResult> DeletePaymentAsync()
        //{
        //    return Ok();
        //}

    }
}
