using Application.ViewModel;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IPaymentService
    {
        Task<string> CreateVNPayUrl(Transationrequest transaction);
        Task<bool> ValidateSignature(string queryString, string signature);
    }
}
