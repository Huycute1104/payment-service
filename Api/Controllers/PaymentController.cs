using Application.IService;
using Application.Utils.GenerateCode;
using Application.ViewModel;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using System.Web;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/payment")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;
        private readonly IGenerateCode _generateCode;

        public PaymentController(IPaymentService paymentService, IConfiguration configuration, IGenerateCode generateCode)
        {
            _paymentService = paymentService;
            _configuration = configuration;
            _generateCode = generateCode;
        }

        [HttpPost("vnpays")]
        public async Task<IActionResult> CreateVNPayUrl([FromBody] Transationrequest transaction)
        {
            var paymentUrl = await _paymentService.CreateVNPayUrl(transaction);
            return Ok(new { paymentUrl });
        }

        [HttpGet("callback")]
        public async Task<IActionResult> PaymentCallback([FromQuery] VnPaymentCallbackModel request)
        {
            if (request.Success)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        // decode info from url
                        /*var orderInfo = HttpUtility.UrlDecode(request.OrderInfo);
                        var parameters = HttpUtility.ParseQueryString(orderInfo);
                        var amount = Guid.Parse(parameters["Amount"]);*/
                        var transation = new PaymentTransaction
                        {
                            Amount = request.Amount,
                            CreatedAt = DateTime.UtcNow,
                            OrderId = _generateCode.GenerateOrderCode(),
                            Status = "Success"
                        };

                        scope.Complete();

                        return Redirect(_configuration["Payment:SuccessUrl"]);
                    }
                    catch (Exception ex)
                    {
                        return Redirect(_configuration["Payment:FailedUrl"]);
                    }
                }
            }
            else
            {
                return Redirect(_configuration["Payment:FailedUrl"]);
            }
        }
    }

}
