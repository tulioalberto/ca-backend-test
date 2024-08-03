using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Nexer.Business.Interfaces;
using Nexer.Business.Notifications;
using System.Net;

namespace Nexer.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificator _notificator;

        protected MainController(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected bool ValidOperation()
        {
            return !_notificator.HaveNotification();
        }

        protected ActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK,  object result = null)
        {
            if (ValidOperation())
            {
                return new ObjectResult(result)
                {
                    StatusCode = Convert.ToInt32(statusCode),
                };
            }

            return BadRequest(new
            {
                erros = _notificator.GetNotifications().Select(n => n.Message)
            });
        }
        protected ActionResult CustomResponse(ModelStateDictionary modelState) 
        {
            if (!modelState.IsValid) NotifyErroModelInvalid(modelState);
            return CustomResponse();
        }

        protected void NotifyErroModelInvalid(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyErrors(errorMsg);
            }
        }

        protected void NotifyErrors(string message)
        {
            _notificator.Handle(new Notification(message));
        }
    }
}
