using Domain.Contracts.Repositories;
using Domain.Entities;
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
    public class EmployerRepository : IEmployerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployerRepository> _logger;

        public EmployerRepository(ApplicationDbContext context, ILogger<EmployerRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<Employer> CreateAsync(Employer employer)
        {
            await _context.Employers.AddAsync(employer);
            return employer;
        }

        public Task DeleteAsync(Employer employer)
        {
            _context.Entry(employer).State = EntityState.Deleted;
            return Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(string companyName)
        {
            return await _context.Employers.AnyAsync(e => e.CompanyName == companyName);
        }

        public async Task<Employer> GetByIdAsync(Guid id)
        {
            return await _context.Employers.FindAsync(id);
        }

        public async Task<Employer> GetByRegistrationNumberAsync(string registrationNumber)
        {
            return await _context.Employers
            .SingleOrDefaultAsync(e => e.RegistrationNumber == registrationNumber);
        }

        public Task<Employer> UpdateAsync(Guid id, Employer employer)
        {
            _context.Entry(employer).State = EntityState.Modified;
            return Task.FromResult(employer);
        }
    }
}
