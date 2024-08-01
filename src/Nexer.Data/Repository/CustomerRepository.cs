

using Microsoft.EntityFrameworkCore;
using Nexer.Business.Interfaces;
using Nexer.Business.Models;
using Nexer.Data.Context;

namespace Nexer.Data.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(MyDbContext context) : base(context) { }

        public async Task<Customer> GetCustomerAndProducts(Guid id)
        {
            return await Db.Customers.AsNoTracking()
                .Include(x => x.Products)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
