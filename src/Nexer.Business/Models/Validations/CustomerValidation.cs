using FluentValidation;

namespace Nexer.Business.Models.Validations
{
    public class CustomerValidation : AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("The field {PropertyName} cannot be empty.")
                .Length(2, 100).WithMessage("The field {PropertyName} need to be between {MinLength} and {MaxLength}.");

            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("The field {PropertyName} cannot be empty.")
                .Length(2, 100).WithMessage("The field {PropertyName} need to be between {MinLength} and {MaxLength}.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("The field {PropertyName} cannot be empty.")
                .Length(2, 100).WithMessage("The field {PropertyName} needs to be between {MinLength} and {MaxLength}.")
                .EmailAddress().WithMessage("The field {PropertyName} must be a valid email address.");
        }
    }
}
