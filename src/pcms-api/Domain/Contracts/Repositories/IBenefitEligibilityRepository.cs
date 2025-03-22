using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IBenefitEligibilityRepository
    {
        Task<BenefitEligibility> UpdateBenefitEligibility(Guid id, BenefitEligibility benefitEligibility);
        Task<BenefitEligibility> CheckIfMemberIsEligibleAsync(Guid id, Guid Id);
    }
}
