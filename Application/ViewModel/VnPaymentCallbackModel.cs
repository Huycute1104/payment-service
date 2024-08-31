using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel
{
    public class VnPaymentCallbackModel
    {
        [FromQuery(Name = "vnp_TransactionStatus")]
        public string? TransactionStatus { get; set; }

        [FromQuery(Name = "vnp_TransactionNo")]
        public string? TransactionNo { get; set; }

        [FromQuery(Name = "vnp_TxnRef")]
        public string? TransactionCode { get; set; }

        [FromQuery(Name = "vnp_ResponseCode")]
        public string? ResponseCode { get; set; }

        [FromQuery(Name = "vnp_OrderInfo")]
        public string? OrderInfo { get; set; }

        [FromQuery(Name = "vnp_Amount")]
        public decimal Amount { get; set; }

        public bool Success => "00".Equals(ResponseCode);
    }
}
