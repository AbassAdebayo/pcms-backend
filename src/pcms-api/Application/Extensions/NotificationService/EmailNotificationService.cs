using Domain.Contracts.Repositories;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions.NotificationService
{
    public class EmailNotificationService : INotificationService
    {
        private readonly ILogger<EmailNotificationService> _logger;
        

        public EmailNotificationService(ILogger<EmailNotificationService> logger)
        {
            
            _logger = logger;
        }

        public async Task SendAlert(string message)
        {
            //var members = await _memberRepository.GetByIdAsync();
            _logger.LogInformation($"Sending email to Member {message}");

            //Simulate email sending 
            await Task.Delay(1000);
        }

        public async Task SendNotification(Guid memberId, string message)
        {
            _logger.LogWarning($"ALERT: {message}");

            //Simulate system-wide alert (e.g., admin notification)
            await Task.Delay(1000);
        }
    }
}
