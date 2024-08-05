using Nexer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexer.Business.Interfaces
{
    public interface IBillingRepository : IRepository<Billing> 
    {
        Task<Billing> GetBillingWithLines(Guid id);
        Task<bool> ExistsByInvoiceNumberAsync(string invoiceNumber);
    }
}
