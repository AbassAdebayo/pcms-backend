using Application.Abstractions;
using Domain.Contracts.Repositories;
using Domain.Wrapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Employer
{
    public class GetEmployerByIdQueryHandler : IQueryHandler<GetEmployerByIdQuery, GetEmployerByIdQueryResponse>
    {
        private readonly IEmployerRepository _employerRepository;
        private readonly ILogger<GetEmployerByIdQueryHandler> _logger;
        public GetEmployerByIdQueryHandler(IEmployerRepository employerRepository, ILogger<GetEmployerByIdQueryHandler> logger)
        {
            _employerRepository = employerRepository;
            _logger = logger;
        }
        public async Task<Result<GetEmployerByIdQueryResponse>> Handle(GetEmployerByIdQuery request, CancellationToken cancellationToken)
        {
            var employer = await _employerRepository.GetByIdAsync(request.Id);
            if (employer == null)
            {
                _logger.LogWarning($"Employer with Id {request.Id} not found");
                return await Result<GetEmployerByIdQueryResponse>.FailAsync($"Employer with Id {request.Id} not found");
            }

            _logger.LogInformation($"Employer with Id {request.Id} found");
            var data = new GetEmployerByIdQueryResponse(employer.Id, employer.CompanyName, employer.RegistrationNumber, employer.Status);

            return await Result<GetEmployerByIdQueryResponse>.SuccessAsync(data);
        }
    }
}
