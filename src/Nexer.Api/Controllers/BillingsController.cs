using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Nexer.Api.ViewModels;
using Nexer.Business.Interfaces;
using Nexer.Business.Models;
using Nexer.Business.Services;
using Nexer.Data.Repository;
using System.Net;

namespace Nexer.Api.Controllers
{
    [Route("api/billings")]
    public class BillingsController : MainController
    {
        
        private readonly IBillingService _billingService;
        private readonly IMapper _mapper;
        private readonly IExternalBillingService _externalBillingService;
        private readonly IBillingRepository _billingRepository;

        public BillingsController(INotificator notificator,
                                  IExternalBillingService externalBillingService,
                                  IMapper mapper,
                                  IBillingRepository billingRepository) : base(notificator)
        {
            _externalBillingService = externalBillingService;
            _mapper = mapper;
            _billingRepository = billingRepository;
        }

        [HttpPost("importBillingToLocalDb")]
        public async Task<IActionResult> ImportBillings(Guid customerId)
        {
            await _externalBillingService.ImportBillingsAsync(customerId);

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            return CustomResponse(HttpStatusCode.OK, "Importação realizada com sucesso");
        }

        [HttpGet]
        public async Task<IEnumerable<BillingViewModel>> GetAll()
        {
            var billings = await _billingRepository.GetAll();
            return _mapper.Map<IEnumerable<BillingViewModel>>(billings);
        }
    }
}
