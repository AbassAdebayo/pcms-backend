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
using System.Transactions;

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

        public async Task<decimal> CalculateInterest(Guid memberId)
            {
                var totalContributions = await _context.Contributions
             .Where(c => c.MemberId == memberId)
             .SumAsync(c => c.Amount);

            if(totalContributions <= 0) return 0;
    
            decimal annualRate = 0.05m; 
            int months = 12; 
            decimal timeInYears = 1;

            // Interest formula: A = P(1 + r/n)^(nt)
            decimal finalAmount = totalContributions * (decimal)Math.Pow((double)(1 + annualRate / months), months * (double)timeInYears);

            // Interest earned
            decimal interestEarned = finalAmount - totalContributions;

            return interestEarned;

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

        public async Task<List<Contribution>> GetFailedTransactions()
        {
            return await _context.Contributions
                .Where(c => c.ContributionStatus == Domain.Enums.ContributionStatus.Failed && c.RetryCount < 3)
                .ToListAsync();
        }

        public async Task<List<Contribution>> GetInvalidContributions()
        {
            return await _context.Contributions
                .Where(c => c.Amount <= 0)
                .ToListAsync();
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

        public async Task<bool> RetryTransaction(Guid contributionId)
        {
            var contribution = await _context.Contributions
                .FindAsync(contributionId);

            if (contribution == null) return false;

            bool success = await ProcessContribution(contribution);
            if (success)
            {
                contribution.ContributionStatus = Domain.Enums.ContributionStatus.Completed;
                _logger.LogInformation($"Contribution {contribution.Id} retried successfully.");
            }
            else
            {
                contribution.ContributionStatus = Domain.Enums.ContributionStatus.Failed;
                contribution.RetryCount++;
                _logger.LogWarning($"Retry failed for contribution {contribution.Id}. Attempt #{contribution.RetryCount}");
            }

            await _context.SaveChangesAsync();
            return success;
        }

        public Task<decimal> TotalContributions(Guid memberId)
        {
            return _context.Contributions
                .Where(m => m.MemberId == memberId)
                .SumAsync(c => c.Amount);
        }

        private async Task<bool> ProcessContribution(Contribution contribution)
        {
            await Task.Delay(500); // Simulate processing delay

            // Simulating 70% success rate for retry
            Random rnd = new Random();
            return rnd.Next(0, 10) > 3; // 70% success, 30% failure
        }
    }
}
