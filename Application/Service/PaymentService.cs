using Application.IService;
using Application.Utils.VNPay;
using Application.ViewModel;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Application.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public PaymentService(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public Task<string> CreateVNPayUrl(Transationrequest transaction)
        {
            HttpContext context = _contextAccessor.HttpContext;
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.UtcNow.Ticks.ToString();
            var pay = new VnPayLibrary();
            var urlCallBack = $"{_configuration["VnPay:CallbackUrl"]}";

            int multipliedPrice = (int)(transaction.Amount * 100);
            string multipliedPriceString = multipliedPrice.ToString();

            pay.AddRequestData("vnp_Version", _configuration["VnPay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["VnPay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["VnPay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", multipliedPriceString);
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["VnPay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["VnPay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", $"Amount={transaction.Amount}");
            pay.AddRequestData("vnp_OrderType", "VNPay");
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl = pay.CreateRequestUrl(_configuration["VnPay:BaseUrl"], _configuration["VnPay:HashSecret"]);
            Console.WriteLine("Generated VNPay URL: " + paymentUrl);

            return Task.FromResult(paymentUrl);
        }


        public Task<bool> ValidateSignature(string queryString, string signature)
        {
            var pay = new VnPayLibrary();
            foreach (var param in queryString.TrimStart('?').Split('&'))
            {
                var keyValue = param.Split('=');
                if (keyValue.Length == 2)
                {
                    pay.AddResponseData(keyValue[0], keyValue[1]);
                }
            }

            bool isValidSignature = pay.ValidateSignature(signature, _configuration["VnPay:HashSecret"]);
            return Task.FromResult(isValidSignature);
        }
    }
}
