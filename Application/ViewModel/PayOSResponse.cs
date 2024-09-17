using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel
{
    public class PayOSResponse
    {
        public int Error { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }

        public PayOSResponse(int error, string message, object? data)
        {
            Error = error;
            Message = message;
            Data = data;

        }
    }
}
