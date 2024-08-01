using System.ComponentModel.DataAnnotations;

namespace Nexer.Api.ViewModels
{
    public class CustomerViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field need to have between {2} and {1} characters ", MinimumLength = 2)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(1000, ErrorMessage = "The field needs to have between {2} and {1} characters", MinimumLength = 2)]
        [EmailAddress(ErrorMessage = "The field {0} must be a valid email address")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(1000, ErrorMessage = "The field need to have betwrrn {2} and {1} characters ", MinimumLength = 2)]
        public string? Address { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}
