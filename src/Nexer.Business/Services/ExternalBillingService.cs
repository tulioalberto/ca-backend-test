using Newtonsoft.Json;
using Nexer.Business.Interfaces;
using Nexer.Business.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nexer.Business.Services
{
    public class ExternalBillingService : BaseService, IExternalBillingService
    {
        private readonly HttpClient _httpClient;
        private readonly IBillingService _billingService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBillingRepository _billingRepository;

        public ExternalBillingService(HttpClient httpClient,
                                      IBillingService billingService,
                                      ICustomerService customerService,
                                      IProductService productService,
                                      IProductRepository productRepository,
                                      ICustomerRepository customerRepository,
                                      INotificator notificator,
                                      IBillingRepository billingRepository) : base(notificator)
        {
            _httpClient = httpClient;
            _billingService = billingService;
            _customerService = customerService;
            _productService = productService;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _billingRepository = billingRepository;
        }

        public async Task ImportBillingsAsync(Guid customerId, Guid? productId = null)
        {
            var response = await _httpClient.GetAsync("https://65c3b12439055e7482c16bca.mockapi.io/api/v1/billing");
            response.EnsureSuccessStatusCode();

            var billingDtos = JsonConvert.DeserializeObject<List<BillingDto>>(await response.Content.ReadAsStringAsync());

            bool foundMatchingBilling = false;

            foreach (var billingDto in billingDtos.Where(b => b.Customer.Id == customerId))
            {

                if (await _billingRepository.ExistsByInvoiceNumberAsync(billingDto.invoice_number))
                {
                    Notificate($"Billing with InvoiceNumber {billingDto.invoice_number} already exists.");
                    return;
                }

                var filteredBillingLines = billingDto.Lines;

                if (filteredBillingLines == null || !filteredBillingLines.Any()) continue;

                var productIds = filteredBillingLines.Select(bl => bl.ProductId).ToList();

                var existingProductIds = await _productRepository.GetProductIdsAsync(productIds);

                var missingProductIds = productIds.Except(existingProductIds).ToList();
                if (missingProductIds.Any())
                {
                    var missingProductNames = filteredBillingLines
                        .Where(bl => missingProductIds.Contains(bl.ProductId))
                        .Select(bl => bl.Description)
                        .ToList();

                    Notificate($"Customer {billingDto.Customer.Name} doess not have the following items registered in the local database: {string.Join(", ", missingProductNames)}");
                    return;
                }

                var billing = new Billing
                {
                    Id = Guid.NewGuid(),
                    InvoiceNumber = billingDto.invoice_number,
                    Date = billingDto.Date,
                    DueDate = billingDto.due_date,
                    TotalAmount = billingDto.total_amount,
                    Currency = billingDto.Currency,
                    CustomerId = customerId,
                    BillingLines = filteredBillingLines.Select(bl => new BillingLine
                    {
                        Id = Guid.NewGuid(),
                        BillingLineId = Guid.NewGuid(),
                        ProductId = bl.ProductId,
                        Description = bl.Description,
                        Quantity = bl.Quantity,
                        UnitPrice = bl.UnitPrice,
                        Subtotal = bl.Subtotal
                    }).ToList()
                };

                try
                {
                    await _billingService.Add(billing);
                    foundMatchingBilling = true;
                    break;
                }
                catch (Exception ex)
                {
                    Notificate($"An error occurred while adding the billing: {ex.Message}");
                }
            }

            if (!foundMatchingBilling)
            {
                Notificate("No matching billing found for the provided customer and/or product.");
            }
        }
    }

    public class BillingDto
    {
        public string invoice_number { get; set; }
        public CustomerDto Customer { get; set; }
        public DateTime Date { get; set; }
        public DateTime due_date { get; set; }
        public decimal total_amount { get; set; }
        public string Currency { get; set; }
        public List<BillingLineDto> Lines { get; set; }
    }

    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class BillingLineDto
    {
        public Guid ProductId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }
}
