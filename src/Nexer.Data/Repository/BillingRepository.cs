using Microsoft.EntityFrameworkCore;
using Nexer.Business.Interfaces;
using Nexer.Business.Models;
using Nexer.Data.Context;

namespace Nexer.Data.Repository
{
    public class BillingRepository : Repository<Billing>, IBillingRepository
    {
        public BillingRepository(MyDbContext context) : base(context) { }

        public async Task<Billing> GetBillingWithLines(Guid id)
        {
            return await Db.Billings.AsNoTracking()
                .Include(b => b.BillingLines)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> ExistsByInvoiceNumberAsync(string invoiceNumber)
        {
            return await Db.Billings.AnyAsync(b => b.InvoiceNumber == invoiceNumber);
        }

        public override async Task<List<Billing>> GetAll()
        {
            return await Db.Billings.AsNoTracking()
                .Include(b => b.BillingLines)
                    .ThenInclude(bl => bl.Product)
                .Include(b => b.Customer)
                .ToListAsync();
        }
    }
}
