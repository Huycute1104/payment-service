using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("vnpays")]
        public async Task<IActionResult> CreateVNPayUrl([FromBody] PaymentTransaction transaction)
        {
            var paymentUrl = await _paymentService.InitiatePayment(transaction);
            return Ok(new { paymentUrl });
        }

        [HttpGet("callback")]
        public async Task<IActionResult> VNPayCallback([FromQuery] string vnp_OrderId, [FromQuery] string vnp_TransactionNo)
        {
            var isVerified = await _paymentService.VerifyPayment(vnp_OrderId, vnp_TransactionNo);
            if (isVerified)
            {
                return Ok("Payment Verified");
            }
            else
            {
                return BadRequest("Payment Verification Failed");
            }
        }
    }

}
