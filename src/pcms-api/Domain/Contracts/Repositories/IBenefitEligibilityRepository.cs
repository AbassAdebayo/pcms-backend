using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IBenefitEligibilityRepository
    {
        Task UpdateBenefitEligibility(Guid memberId, EligibilityStatus eligibilityStatus);
        Task<BenefitEligibility> GetByMemberIdAsync(Guid memberId);
        public Task<List<BenefitEligibility>> GetByMembersAsync();
    }
}
