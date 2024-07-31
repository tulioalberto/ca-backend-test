
using Nexer.Business.Models;

namespace Nexer.Business.Interfaces
{
    public interface ICustomerService : IDisposable
    {
        Task Add(Customer customer);
        Task Update(Customer customer);
        Task Remove(Guid id);
    }
}
