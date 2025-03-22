using Domain.Entities;
using Domain.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IMemberRepository
    {
        Task<Member> CreateAsync(Member member);
        Task<Member> UpdateAsync(Guid id, Member member);
        Task DeleteAsync(Member member);
        Task<Member> GetByIdAsync(Guid id);
        Task<Member> GetByNameAsync(string name, Guid employerId);
        Task<bool> ExistsAsync(string email);
        Task<PaginatedResult<Member>> ListAsync(int page, int pageSize);
        Task<PaginatedResult<Member>> ListAsync(int page, int pageSize, Guid employerId);
        
    }
}
