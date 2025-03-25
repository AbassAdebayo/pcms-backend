using Application.Extensions.NotificationService;
using Application.Services.BenefitEligibilityService;
using Domain.Contracts.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions.BackgroundService
{
    public class BackgroundService
    {
        private readonly IContributionRepository _contributionRepository;
        private readonly IBenefitEligibilityService _eligibilityService;
        private readonly INotificationService _notificationService;
        private readonly ILogger<BackgroundService> _logger;

        public BackgroundService(IContributionRepository contributionRepository, IBenefitEligibilityService benefitEligibilityService,
            INotificationService notificationService, ILogger<BackgroundService> logger)
        {
            _contributionRepository = contributionRepository;
            _eligibilityService = benefitEligibilityService;
            _notificationService = notificationService;
            _logger = logger;
        }


        //Notify on Failed Transactions
        public async Task HandleFailedTransactions()
        {
            _logger.LogInformation("Checking for failed transactions...");

            var failedTransactions = await _contributionRepository.GetFailedTransactions();
            if (!failedTransactions.Any())
            {
                _logger.LogInformation("No failed transactions found.");
                return;
            }

            foreach (var transaction in failedTransactions)
            {
                _logger.LogError($"Failed transaction: {transaction.Id}");

                // Notify user
                await _notificationService.SendNotification(transaction.MemberId,
                    "Your recent transaction has failed. Please review your account.");

                // Retry logic
                bool retrySuccess = await _contributionRepository.RetryTransaction(transaction.Id);
                if (retrySuccess)
                {
                    _logger.LogInformation($"Successfully retried transaction: {transaction.Id}");
                }
                else
                {
                    _logger.LogWarning($"Retry failed for transaction: {transaction.Id}");
                    await _notificationService.SendAlert($"Transaction {transaction.Id} failed permanently.");
                }
            }
        }

        // ✅ Notify on Eligibility Updates
        public async Task UpdateBenefitEligibility(Guid memberId)
        {
            _logger.LogInformation("Starting benefit eligibility update job...");

            var members = await _eligibilityService.CheckMembersEligibilityAsync();
            foreach (var member in members)
            {
                var eligibilityStatus = await _eligibilityService.CheckEligibilityAsync(memberId);
                await _eligibilityService.UpdateBenefitEligibilityAsync(memberId, eligibilityStatus);
                _logger.LogInformation($"Updated eligibility for Member ID: {memberId}");

                // Notify member
                await _notificationService.SendNotification(memberId,
                    "Congratulations! You are now eligible for pension benefits.");
            }
        }
    }
   
}
