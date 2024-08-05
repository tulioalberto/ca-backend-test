using System.ComponentModel.DataAnnotations;

namespace Nexer.Api.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        //[Required(ErrorMessage = "The field {0} is required")]
        public Guid? CustomerId { get; set; }
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage ="The field need to have between {2} and {1} characters ", MinimumLength = 2)]
        public string? Name { get; set; }

        //[Required(ErrorMessage = "The field {0} is required")]
        //[StringLength(1000, ErrorMessage = "The field need to have betwen {2} and {1} characters ", MinimumLength = 2)]
        //public string? Description { get; set; }
    }
}
