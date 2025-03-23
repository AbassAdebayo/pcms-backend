using Domain.Contracts.Repositories;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Application.Abstractions.ICommand;

namespace Application.Commands.Employer
{
    public class CreateEmployerCommandHandler : ICommand<CreateEmployerCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployerRepository _employerRepository;
        private readonly ILogger<CreateEmployerCommandHandler> _logger;

        public CreateEmployerCommandHandler(IUnitOfWork unitOfWork, IEmployerRepository employerRepository, ILogger<CreateEmployerCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _employerRepository = employerRepository;
            _logger = logger;
        }

        public async Task<CreateEmployerCommandResponse> Handle(CreateEmployerCommand request, CancellationToken token)
        {
            //check if pfa exist
            var employerExists = await _employerRepository.ExistsAsync(request.companyName);
            if (employerExists)
            {
                _logger.LogWarning($"Employer with Name {request.companyName} already exists");
                return new CreateEmployerCommandResponse()
                {
                    Message = $"Employer with Name {request.companyName} already exists"
                };
            }

            //create PFA
            var employer = new Domain.Entities.Employer(new Guid(), request.companyName, GenerateRegistrationNumber(request.companyName), Domain.Enums.Status.Active);
            if (employer == null)
            {
                _logger.LogWarning($"Employer with Name {request.companyName} not created");
                return new CreateEmployerCommandResponse()
                {
                    Message = $"Employer with Name {request.companyName} not created"
                };
            }
            await _employerRepository.CreateAsync(employer);

            //Save to Db
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"{employer.CompanyName}  Created Successfully");

            return new CreateEmployerCommandResponse()
            {
                IsSuccess = true,
                Message = "Employer Created",
                Id = employer.Id,
                CompanyName = employer.CompanyName,
                RegistrationNumber = employer.RegistrationNumber,
                Status = employer.Status
            };
        }

        private string GenerateRegistrationNumber(string companyName)
        {
            return companyName.Substring(0, 3).ToUpper() + "-" + Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
        }

    }
}
