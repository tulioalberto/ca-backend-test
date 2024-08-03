using Nexer.Business.Interfaces;
using Nexer.Business.Models;
using Nexer.Business.Models.Validations;

namespace Nexer.Business.Services
{
    public class CustomerService : BaseService, ICustomerService //implementa o contrato e os metodos
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository,
            INotificator notificator) : base(notificator)
        {
            _customerRepository = customerRepository;
        }
        public async Task Add(Customer customer)
        {
            //Validar se a entidade é consistente..
            if (!ExecuteValidation(new CustomerValidation(), customer))
                return;

            if(_customerRepository.Search(c => c.Id == customer.Id).Result.Any())
            {
                Notificate("Already exist this customer in the Database");
                return;
            }

            await _customerRepository.Add(customer);
        }

        public async Task Update(Customer customer)
        {
            if (!ExecuteValidation(new CustomerValidation(), customer))
                return;

            await _customerRepository.Update(customer);
        }

        public async Task Remove(Guid id)
        {
            var customer = await _customerRepository.GetCustomerAndProducts(id);

            if(customer == null)
            {
                Notificate("The customer does not exist.");
                return;
            }

            if(customer.Products.Any())
            {
                Notificate("The customer have associated products.");
                return;
            }     
            
            await _customerRepository.Remove(id);
        }

        //Caso não seja null, tira da memoria (garbabve collector)
        public void Dispose()
        {
            _customerRepository?.Dispose();
        }
    }
}
