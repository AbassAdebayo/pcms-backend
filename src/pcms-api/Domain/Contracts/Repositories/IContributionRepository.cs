﻿using Domain.Entities;
using Domain.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IContributionRepository
    {
        Task<Contribution> CreateAsync(Contribution contribution);
        Task DeleteAsync(Contribution contribution);
        Task<Contribution> GetByIdAsync(Guid id);
        
        Task<PaginatedResult<Contribution>> ListAsync(int page, int pageSize);
    }
}
