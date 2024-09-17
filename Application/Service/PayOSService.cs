using Application.IService;
using Application.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Net.payOS;
using Net.payOS.Types;


namespace Application.Service
{
    public class PayOSService : IPayOSService
    {
        private readonly PayOS _payOS;
        private readonly IConfiguration _configuration;

        public PayOSService(PayOS payOS, IConfiguration configuration)
        {
            _payOS = payOS;
            _configuration = configuration;
        }

        public async Task<PayOSResponse> CreatePayOSUrl(PaymentRequest request)
        {
            try
            {
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
                ItemData item = new ItemData("Mì tôm hảo hảo ly", 1, 1000);
                List<ItemData> items = new List<ItemData>();
                PaymentData paymentData = new PaymentData(
                    orderCode,
                    (int)request.Amount,
                    "Thanh Toan Đơn Hàng",
                    items,
                    _configuration["PayOS:CancelUrl"],
                    _configuration["PayOS:ReturnUrl"] 
                );

                CreatePaymentResult paymentUrl = await _payOS.createPaymentLink(paymentData);

                return new PayOSResponse(0, "Success", paymentUrl);
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception);
                return new PayOSResponse(-1, exception.Message, null);
            }
        }
    }
}
