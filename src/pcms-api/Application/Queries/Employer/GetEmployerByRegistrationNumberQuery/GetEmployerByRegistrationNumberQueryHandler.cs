using Application.Abstractions;
using Domain.Contracts.Repositories;
using Domain.Wrapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Employer.GetEmployerByRegistrationNumberQuery
{
    public class GetEmployerByRegistrationNumberQueryHandler : IQueryHandler<GetEmployerByRegistrationNumberQuery, GetEmployerByRegistrationNumberQueryResponse>
    {
        private readonly IEmployerRepository _employerRepository;
        private readonly ILogger<GetEmployerByRegistrationNumberQueryHandler> _logger;
        public GetEmployerByRegistrationNumberQueryHandler(IEmployerRepository employerRepository, ILogger<GetEmployerByRegistrationNumberQueryHandler> logger)
        {
            _employerRepository = employerRepository;
            _logger = logger;
        }
        public async Task<Result<GetEmployerByRegistrationNumberQueryResponse>> Handle(GetEmployerByRegistrationNumberQuery request, CancellationToken cancellationToken)
        {
            var employer = await _employerRepository.GetByRegistrationNumberAsync(request.registrationNumber);
            if (employer == null)
            {
                _logger.LogWarning($"Employer with Registration Number {request.registrationNumber} not found");
                return await Result<GetEmployerByRegistrationNumberQueryResponse>.FailAsync($"Employer with Registration Number {request.registrationNumber} not found");
            }

            _logger.LogInformation($"Employer with Registration Number {request.registrationNumber} found");
            var data = new GetEmployerByRegistrationNumberQueryResponse(employer.Id, employer.CompanyName, employer.RegistrationNumber, employer.Status);

            return await Result<GetEmployerByRegistrationNumberQueryResponse>.SuccessAsync(data);
        }

    }
}