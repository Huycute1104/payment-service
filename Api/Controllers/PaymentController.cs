using Application.IService;
using Application.Service;
using Application.Utils.GenerateCode;
using Application.ViewModel;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPayOSService _payOSService;


        public PaymentController(
            IPaymentService paymentService, 
            IConfiguration configuration, 
            IGenerateCode generateCode,
            IUnitOfWork unitOfWork,
            IPayOSService payOSService)
        {
            _paymentService = paymentService;
            _configuration = configuration;
            _generateCode = generateCode;
            _unitOfWork = unitOfWork;
            _payOSService = payOSService;
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
                            OrderId = "123",
                            Status = "Success"
                        };
                        _unitOfWork.PaymentTransactionRepository.Insert(transation);
                         _unitOfWork.Save();
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

        [HttpPost("payos")]
        public async Task<IActionResult> CreatePayOS([FromBody] PaymentRequest request)
        {
            var paymentUrl = await _payOSService.CreatePayOSUrl(request);
            return Ok(new { Url = paymentUrl });
        }
        [HttpGet("cancel")]
        public async Task<IActionResult> PayOSCancel()
        {
            // logic sau khi thanh toán thành công
            return Redirect(_configuration["Payment:FailedUrl"]);
        }
        [HttpGet("success")]
        public async Task<IActionResult> PayOSSuccess()
        {
            // logic sau khi thanh toán thành công
            return Redirect(_configuration["Payment:SuccessUrl"]);
       
        }
    }

}
