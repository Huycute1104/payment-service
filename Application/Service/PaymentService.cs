using Domain.Entities;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class PaymentService : IPaymentService
    {
        public Task<string> CreateVNPayUrl(PaymentTransaction transaction)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidateSignature(string queryString, string signature)
        {
            throw new NotImplementedException();
        }
    }

}
