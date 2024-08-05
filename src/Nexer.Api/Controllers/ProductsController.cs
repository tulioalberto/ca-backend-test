using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nexer.Api.ViewModels;
using Nexer.Business.Interfaces;
using Nexer.Business.Models;
using System.Net;

namespace Nexer.Api.Controllers
{
    [Route("api/products")]
    public class ProductsController : MainController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository,
                                  IProductService productService,
                                  IMapper mapper,
                                  INotificator notificator) : base (notificator) 
        {
            _productRepository = productRepository;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsByCustomer());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> GetById(Guid id)
        {
            var productViewModel = await GetProduct(id);
            if(productViewModel == null) return NotFound();

            return productViewModel;
        }

        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> Add(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _productService.Add(_mapper.Map<Product>(productViewModel));

            return CustomResponse(HttpStatusCode.Created, productViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> Update(Guid id, ProductViewModel productViewModel)
        {
            if(id != productViewModel.Id)
            {
                NotifyErrors("The Id is not equal(Os Ids nao sao iguais)");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var productUpdate = await GetProduct(id);

            productUpdate.Name = productViewModel.Name;
            //productUpdate.Description = productViewModel.Description;

            await _productRepository.Update(_mapper.Map<Product>(productUpdate));

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProductViewModel>> Delete(Guid id)
        {
            var product = await GetProduct(id);

            if(product == null) return NotFound();

            await _productService.Remove(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }

        private async Task<ProductViewModel> GetProduct(Guid id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetProductCustomer(id));
        }
    }
}
