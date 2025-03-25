using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BenefitEligibilityRepository : IBenefitEligibilityRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<BenefitEligibilityRepository> _logger;

        public BenefitEligibilityRepository(ApplicationDbContext context, ILogger<BenefitEligibilityRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<BenefitEligibility> GetByMemberIdAsync(Guid memberId)
        {
            return await _context.BenefitEligibilities
                .FirstOrDefaultAsync(b => b.MemberId == memberId);
        }

        public async Task<List<BenefitEligibility>> GetByMembersAsync()
        {
            return await _context.BenefitEligibilities
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateBenefitEligibility(Guid memberId, EligibilityStatus eligibilityStatus)
        {
            var eligibility = await _context.BenefitEligibilities
            .FirstOrDefaultAsync(be => be.MemberId == memberId);

            if (eligibility != null)
            {
                eligibility.EligibilityStatus = eligibilityStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
}
