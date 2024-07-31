using Nexer.Business.Models;

namespace Nexer.Business.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomerAndProducts(Guid id);
    }
}
