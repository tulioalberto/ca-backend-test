
using Microsoft.EntityFrameworkCore;
using Nexer.Business.Interfaces;
using Nexer.Business.Models;
using Nexer.Data.Context;

namespace Nexer.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MyDbContext context) : base(context) { }

        public async Task<Product> GetProductCustomer(Guid id)
        {
            return await Db.Products.AsNoTracking()
                .Include(f => f.Customer)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCustomer()
        {
            return await Db.Products.AsNoTracking()
                .Include(f => f.Customer)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }
    }
}
