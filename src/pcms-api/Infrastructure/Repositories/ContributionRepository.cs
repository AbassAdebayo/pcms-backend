using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Context;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ContributionRepository : IContributionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ContributionRepository> _logger;

        public ContributionRepository(ApplicationDbContext context, ILogger<ContributionRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Contribution> CreateAsync(Contribution contribution)
        {
            await _context.Contributions.AddAsync(contribution);
            return contribution;
        }

        public Task DeleteAsync(Contribution contribution)
        {
            _context.Entry(contribution).State = EntityState.Deleted;
            return Task.FromResult(true);
        }
        public async Task<Contribution> GetByIdAsync(Guid id)
        {
           return await _context.Contributions
                .FindAsync(id);
        }

        public async Task<bool> HasMonthlyContribution(Guid memberId, DateTime contributionDate)
        {
            return await _context.Contributions
                .AnyAsync(c => c.MemberId == memberId && c.ContributionType == Domain.Enums.ContributionType.MonthlyContributions && c.ContributionDate.Year == contributionDate.Year && c.ContributionDate.Month == contributionDate.Month);
        }

        public async Task<PaginatedResult<Contribution>> ListAsync(int page, int pageSize)
        {
            return await _context.Contributions
                .OrderByDescending(c => c.ContributionDate)
                .AsNoTracking()
                .ToPaginatedResultListAsync(page, pageSize);
           
        }

        public async Task<PaginatedResult<Contribution>> ListAsync(Guid memberId, int page, int pageSize)
        {
            return await _context.Contributions
                .Where(m => m.MemberId == memberId)
                .OrderByDescending(c => c.ContributionDate)
                .AsNoTracking()
                .ToPaginatedResultListAsync(page, pageSize);
        }

        public Task<decimal> TotalContributions(Guid memberId)
        {
            return _context.Contributions
                .Where(m => m.MemberId == memberId)
                .SumAsync(c => c.Amount);
        }
    }
}
