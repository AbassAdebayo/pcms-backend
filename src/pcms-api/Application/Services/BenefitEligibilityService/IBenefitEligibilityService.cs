using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BenefitEligibilityService
{
    public interface IBenefitEligibilityService
    {
        public Task<EligibilityStatus> CheckEligibilityAsync(Guid memberId);
        public Task<List<EligibilityStatus>> CheckMembersEligibilityAsync();
        public Task UpdateBenefitEligibilityAsync(Guid memberId, EligibilityStatus eligibilityStatus);
    }
}
