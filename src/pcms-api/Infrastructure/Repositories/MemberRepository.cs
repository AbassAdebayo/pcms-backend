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
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MemberRepository> _logger;

        public MemberRepository(ApplicationDbContext context, ILogger<MemberRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Member> CreateAsync(Member member)
        {
             await _context.Members.AddAsync(member);
            return member;
        }

        public Task DeleteAsync(Member member)
        {
            _context.Entry(member).State = EntityState.Deleted;
            return Task.FromResult(true);
        }
        //public Task DeleteAsync(Member member)
        //{
        //    member.IsDeleted = true;
        //    _context.Update(member);
        //    return Task.CompletedTask;
        //}

        public async Task<bool> ExistsAsync(string email)
        {
            return await _context.Members.AnyAsync(e => e.Email == email);
        }

        public async Task<Member> GetByIdAsync(Guid id)
        {
            return await _context.Members
                .FindAsync(id);
        }

        public async Task<Member> GetByNameAsync(string name, Guid employerId)
        {
            return await _context.Members
                .SingleOrDefaultAsync(e => e.Name == name && e.EmployerId == employerId);
        }

        public async Task<PaginatedResult<Member>> ListAsync(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            return await _context.Members
                .Skip(skip)
                .Take(pageSize)
                .AsNoTracking()
                .ToPaginatedResultListAsync(page, pageSize);
        }
        public async Task<PaginatedResult<Member>> ListAsync(int page, int pageSize, Guid employerId)
        {
            int skip = (page - 1) * pageSize;

            return await _context.Members
                .Where(m => m.EmployerId == employerId)
                .Skip(skip)
                .Take(pageSize)
                .AsNoTracking()
                .ToPaginatedResultListAsync(page, pageSize);
        }

        public Task<Member> UpdateAsync(Guid id, Member member)
        {
            _context.Entry(member).State = EntityState.Modified;
            return Task.FromResult(member);
        }
    }
}
