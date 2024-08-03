using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nexer.Api.ViewModels;
using Nexer.Business.Interfaces;
using Nexer.Business.Models;
using System.Net;

namespace Nexer.Api.Controllers
{
    [Route("api/customers")]
    public class CustomersController : MainController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerRepository customerRepository,
                                  ICustomerService customerService,
                                  IMapper mapper,
                                  INotificator notificator) : base(notificator)
        {
            _customerRepository = customerRepository;
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<CustomerViewModel>>(await _customerRepository.GetAll());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CustomerViewModel>> GetById(Guid id)
        {
            var customer = await GetCustomerProducts(id);

            if (customer == null) return NotFound();

            return customer;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerViewModel>> Add(CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _customerService.Add(_mapper.Map<Customer>(customerViewModel));

            return CustomResponse(HttpStatusCode.Created, customerViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CustomerViewModel>> Update(Guid id, CustomerViewModel customerViewModel)
        {
            if (id != customerViewModel.Id)
            {
                NotifyErrors("The id is not equal (os ids nao sao iguais)");
                return CustomResponse();
            }

            if(!ModelState.IsValid) return CustomResponse(ModelState);

            await _customerService.Update(_mapper.Map<Customer>(customerViewModel));

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CustomerViewModel>> Delete(Guid id)
        {
            await _customerService.Remove(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }
        private async Task<CustomerViewModel> GetCustomerProducts(Guid id)
        {
            return _mapper.Map<CustomerViewModel>(await _customerRepository.GetCustomerAndProducts(id));
        }

    }
}
