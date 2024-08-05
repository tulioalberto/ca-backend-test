using System.ComponentModel.DataAnnotations;

namespace Nexer.Api.ViewModels
{
    public class BillingLineViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Guid BillingId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(200, ErrorMessage = "The field {0} needs to have between {2} and {1} characters", MinimumLength = 1)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(1, int.MaxValue, ErrorMessage = "The field {0} must be greater than zero")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The field {0} must be greater than zero")]
        public decimal UnitPrice { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The field {0} must be greater than zero")]
        public decimal Subtotal { get; set; }

        public ProductViewModel Product { get; set; }
    }
}
