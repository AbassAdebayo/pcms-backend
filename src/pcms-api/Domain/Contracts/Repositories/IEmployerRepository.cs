using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IEmployerRepository
    {
        Task<Employer> CreateAsync(Employer employer);
        Task<Employer> UpdateAsync(Guid id, Employer employer);
        Task DeleteAsync(Employer employer);
        Task<Employer> GetByIdAsync(Guid id);
        Task<Employer> GetByRegistrationNumberAsync(string registrationNumber);
        Task<bool> ExistsAsync(string companyName);
        //Task<PaginatedResult<Employer>> ListAsync(int page, int pageSize);
    }
}
