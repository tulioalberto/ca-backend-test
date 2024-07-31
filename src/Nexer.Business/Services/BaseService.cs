using FluentValidation;
using FluentValidation.Results;
using Nexer.Business.Interfaces;
using Nexer.Business.Models;
using Nexer.Business.Notifications;

namespace Nexer.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificator _notificator;

        protected BaseService(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected void Notificate(ValidationResult validationResult)
        {
            foreach (var item in validationResult.Errors)
            {
                Notificate(item.ErrorMessage);
            }
        }

        protected void Notificate(string message)
        {
            _notificator.Handle(new Notification(message));
        }

        //Classe generica para validação
        protected bool ExecuteValidation<TV, TE>(TV validation, TE entity)
            where TV : AbstractValidator<TE>
            where TE : Entity
        {
            var validator = validation.Validate(entity);

            if (validator.IsValid) return true;

            //lancamento de notificacoss
            Notificate(validator);

            return false;
        }
    }
}
