using System.ComponentModel.DataAnnotations;

namespace Nexer.Api.ViewModels
{
    public class BillingViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(50, ErrorMessage = "The field {0} needs to have between {2} and {1} characters", MinimumLength = 1)]
        public string InvoiceNumber { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The field {0} must be greater than zero")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(10, ErrorMessage = "The field {0} needs to have between {2} and {1} characters", MinimumLength = 1)]
        public string Currency { get; set; }

        public CustomerViewModel Customer { get; set; }

        public IEnumerable<BillingLineViewModel> BillingLines { get; set; }
    }
}