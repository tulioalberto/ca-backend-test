using Nexer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexer.Business.Interfaces
{
    public interface IBillingService : IDisposable
    {
        Task Add(Billing billing);
        Task Update(Billing billing);
        Task Remove(Guid id);
    }
}
