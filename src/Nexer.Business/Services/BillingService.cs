using Nexer.Business.Interfaces;
using Nexer.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexer.Business.Services
{
    public class BillingService : BaseService, IBillingService
    {
        private readonly IBillingRepository _billingRepository;

        public BillingService(IBillingRepository billingRepository, INotificator notificator)
            : base(notificator)
        {
            _billingRepository = billingRepository;
        }

        public async Task Add(Billing billing)
        {
            // Adicione a validação necessária
            await _billingRepository.Add(billing);
        }

        public async Task Update(Billing billing)
        {
            // Adicione a validação necessária
            await _billingRepository.Update(billing);
        }

        public async Task Remove(Guid id)
        {
            await _billingRepository.Remove(id);
        }

        public void Dispose()
        {
            _billingRepository?.Dispose();
        }
    }
}
