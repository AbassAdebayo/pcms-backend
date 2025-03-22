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
                .Select(e => new Contribution
                {
                    Id = e.Id,
                    Amount = e.Amount,
                    ContributionDate = e.ContributionDate,
                    MemberId = e.MemberId,
                }).SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<PaginatedResult<Contribution>> ListAsync(int page, int pageSize)
        {
            return await _context.Contributions
                .Select(c => new Contribution { Id = c.Id, Amount = c.Amount, ContributionDate = c.ContributionDate, MemberId = c.MemberId })
                .AsNoTracking()
                .ToPaginatedResultListAsync(page, pageSize);
           
        }
    }
}
