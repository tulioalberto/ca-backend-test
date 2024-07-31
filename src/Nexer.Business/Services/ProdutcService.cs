
using Nexer.Business.Interfaces;
using Nexer.Business.Models;
using Nexer.Business.Models.Validations;

namespace Nexer.Business.Services
{
    public class ProdutcService : BaseService, IProductService
    {
        private readonly IProductService _productService;

        public ProdutcService(IProductService productService,
            INotificator notificator) : base(notificator)
        {
            _productService = productService;
        }

        public async Task Add(Product product)
        {
            if(!ExecuteValidation(new ProductValidation(), product))
                return;

            await _productService.Add(product);
        }

        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product))
                return;

            await _productService.Update(product);
        }

        public async Task Remove(Guid id)
        {
            await _productService.Remove(id);
        }

        //limpeza do garbage collector
        public void Dispose()
        {
            _productService.Dispose();
        }
    }
}
