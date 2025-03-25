using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions.NotificationService
{
    public interface INotificationService
    {
        Task SendNotification(Guid memberId, string message);
        Task SendAlert(string message);
    }
}
