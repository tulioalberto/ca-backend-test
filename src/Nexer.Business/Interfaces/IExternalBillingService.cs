using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexer.Business.Interfaces
{
    public interface IExternalBillingService
    {
        Task ImportBillingsAsync(Guid customerId);
    }
}
