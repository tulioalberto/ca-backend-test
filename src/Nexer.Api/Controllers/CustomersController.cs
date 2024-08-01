using Microsoft.AspNetCore.Mvc;
using Nexer.Api.ViewModels;

namespace Nexer.Api.Controllers
{
    [Route("api/customers")]
    public class CustomersController : MainController
    {
        public CustomersController()
        {
            
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerViewModel>> GetAll()
        {

        }

        [HttpGet("{id:guid")]
        public async Task<ActionResult<CustomerViewModel>> GetById()
        {

        }

        [HttpPost]
        public async Task<ActionResult<CustomerViewModel>> Add(CustomerViewModel customerViewModel)
        {

        }

        [HttpPut("{id:guid")]
        public async Task<ActionResult<CustomerViewModel>> Update(Guid id, CustomerViewModel customerViewModel)
        {

        }

        [HttpDelete("{id:guid")]
        public async Task<ActionResult<CustomerViewModel>> Delete(Guid id)
        {

        }
    }
}
