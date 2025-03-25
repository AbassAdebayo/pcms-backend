using Domain.Entities;
using Domain.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Domain.Contracts.Repositories
{
    public interface IContributionRepository
    {
        Task<Contribution> CreateAsync(Contribution contribution);
        Task DeleteAsync(Contribution contribution);
        Task<Contribution> GetByIdAsync(Guid id);
        Task<bool> HasMonthlyContribution(Guid memberId, DateTime contributionDate);  
        Task<PaginatedResult<Contribution>> ListAsync(int page, int pageSize);
        Task<PaginatedResult<Contribution>> ListAsync(Guid memberId, int page, int pageSize);
        Task<decimal> TotalContributions(Guid memberId);
        Task<List<Contribution>> GetInvalidContributions();
        Task<List<Contribution>> GetFailedTransactions();
        Task<bool> RetryTransaction(Guid contributionId);
        public Task<decimal> CalculateInterest(Guid memberId);
    }
}
