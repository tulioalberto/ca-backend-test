using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexer.Business.Models
{
    public class BillingLine : Entity
    {
        public Guid BillingLineId { get; set; }
        public Billing Billing { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }
}
