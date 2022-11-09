using Domain.Infraestructure.Notification;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Infraestructure.Controller
{
    [ApiController]
    public class BasicController : ControllerBase
    {
        protected readonly INotification _notification;

        public BasicController(INotification notification)
        {
            this._notification = notification;
        }

        protected void AddNotification(string message)
        {
            this._notification.NotificationAdd(message);
        }

        protected bool HaveNotifications()
        {
            return this._notification.HaveNotifications();
        }

        protected List<string> ListAll()
        {
            return this._notification.ListAll();
        }

        protected bool IsModelValid()
        {
            if (!ModelState.IsValid)
            {
                foreach (var erro in ModelState.Values.SelectMany(v => v.Errors).Where(z => z.ErrorMessage != "").Select(e => e.ErrorMessage))
                {
                    AddNotification(erro);
                }
            }
                return !HaveNotifications();
        }

        protected ActionResult CustomResponse(object response, string message = "")
        {
            if (HaveNotifications())
            {
                return BadRequest(new
                {
                    Errors = ListAll()
                });
            }

            return Ok(new { Response = response, message = message });
        }
    }
}
