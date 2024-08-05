using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexer.Business.Models
{
    public class Billing : Entity
    {
        public string InvoiceNumber { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }
        public IEnumerable<BillingLine> BillingLines { get; set; }
    }
}
