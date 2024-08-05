using Nexer.Business.Interfaces;
using Nexer.Business.Models;
using Nexer.Data.Context;

namespace Nexer.Data.Repository
{
    public class BillingLineRepository : Repository<BillingLine>, IBillingLineRepository
    {
        public BillingLineRepository(MyDbContext context) : base(context) { }
    }
}
