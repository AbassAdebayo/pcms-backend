using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BenefitEligibilityService
{
    public class BenefitEligibilityService : IBenefitEligibilityService
    {
        private readonly IBenefitEligibilityRepository _benefitEligibilityRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IContributionRepository _contributionRepository;
        private readonly ILogger<BenefitEligibilityService> _logger;

        public BenefitEligibilityService(IBenefitEligibilityRepository benefitEligibilityRepository,
            IMemberRepository memberRepository, ILogger<BenefitEligibilityService> logger)
        {
            _benefitEligibilityRepository = benefitEligibilityRepository;
            _memberRepository = memberRepository;
            _logger = logger;
        }
        public async Task<EligibilityStatus> CheckEligibilityAsync(Guid memberId)
        {
            var elegibility = await _benefitEligibilityRepository.GetByMemberIdAsync(memberId);
            if(elegibility == null)
            {
                _logger.LogWarning($"Member with Id {memberId} is not eligible for benefits");
                return EligibilityStatus.Pending;
            }

            return elegibility.EligibilityStatus;
        }

        public async Task<List<EligibilityStatus>> CheckMembersEligibilityAsync()
        {
            var eligibleMembers = await _benefitEligibilityRepository.GetByMembersAsync();
            var listOfEligibleMembers = new List<EligibilityStatus>();

            foreach (var member in eligibleMembers)
            {
                if (member == null)
                {
                    _logger.LogWarning($"Member with Id {member.Id} is not eligible for benefits");
                    listOfEligibleMembers.Add(EligibilityStatus.Pending);
                }
                else
                {
                    listOfEligibleMembers.Add(member.EligibilityStatus);
                }
            }

            return listOfEligibleMembers;
        }

        public async Task UpdateBenefitEligibilityAsync(Guid memberId, EligibilityStatus eligibilityStatus)
        {
            var eligibility = await _benefitEligibilityRepository.GetByMemberIdAsync(memberId);
            if (eligibility == null) return;

            var contributions = await _contributionRepository.TotalContributions(memberId);
            var minRequiredDate = eligibility.EligibileFrom.AddYears(5);

            if (DateTime.UtcNow >= minRequiredDate && contributions >= 100000)
            {
                await _benefitEligibilityRepository.UpdateBenefitEligibility(memberId, EligibilityStatus.Eligible);
                _logger.LogInformation($"Member {memberId} is now eligible for benefits.");
            }
            else
            {
                _logger.LogInformation($"Member {memberId} is still ineligible for benefits.");
            }
        }
    }
}
