
using Nexer.Business.Interfaces;
using Nexer.Business.Models;
using Nexer.Business.Models.Validations;

namespace Nexer.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository,
            INotificator notificator) : base(notificator)
        {
            _productRepository = productRepository;
        }

        public async Task Add(Product product)
        {
            if(!ExecuteValidation(new ProductValidation(), product))
                return;

            await _productRepository.Add(product);
        }

        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product))
                return;

            await _productRepository.Update(product);
        }

        public async Task Remove(Guid id)
        {
            await _productRepository.Remove(id);
        }

        //limpeza do garbage collector
        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}
