using FluentValidation;

namespace Nexer.Business.Models.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation() 
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("The field {PropertyName} cannot be empty.")
                .Length(2, 200).WithMessage("The field {PropertyName} need to be between {MinLength} and {MaxLength}.");

            //RuleFor(c => c.Description)
            //    .NotEmpty().WithMessage("The field {PropertyName} cannot be empty.")
            //    .Length(2, 1000).WithMessage("The field {PropertyName} need to be between {MinLength} and {MaxLength}.");
        }
    }
}
