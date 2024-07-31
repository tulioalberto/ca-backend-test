using Nexer.Business.Models;

namespace Nexer.Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCustomer();
    }
}
