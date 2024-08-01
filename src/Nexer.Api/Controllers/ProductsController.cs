using Microsoft.AspNetCore.Mvc;
using Nexer.Api.ViewModels;

namespace Nexer.Api.Controllers
{
    [Route("api/products")]
    public class ProductsController : MainController
    {
        public ProductsController()
        {

        }

        [HttpGet]
        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {

        }

        [HttpGet("{id:guid")]
        public async Task<ActionResult<ProductViewModel>> GetById()
        {

        }

        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> Add(ProductViewModel productViewModel)
        {

        }

        [HttpPut("{id:guid")]
        public async Task<ActionResult<ProductViewModel>> Update(Guid id, ProductViewModel productViewModel)
        {

        }

        [HttpDelete("{id:guid")]
        public async Task<ActionResult<ProductViewModel>> Delete(Guid id)
        {

        }


    }
}
