using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PaymentTransaction
    {
        public Guid Id { get; set; }
        public float Amount { get; set; }
        public string OrderId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
