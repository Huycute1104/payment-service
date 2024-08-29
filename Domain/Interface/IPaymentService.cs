using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IPaymentService
    {
        Task <string> CreateVNPayUrl(PaymentTransaction transaction);
        Task<bool> ValidateSignature(string queryString, string signature);
    }

}
