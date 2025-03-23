using Domain.Contracts.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Application.Abstractions.ICommand;

namespace Application.Commands.Member
{
    public class CreateMemberCommandHandler : ICommand<CreateMemberCommandResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IEmployerRepository _employerRepository;
        private readonly ILogger<CreateMemberCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CreateMemberCommandHandler(IMemberRepository memberRepository, ILogger<CreateMemberCommandHandler> logger,
            IEmployerRepository employerRepository,  IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _employerRepository = employerRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<CreateMemberCommandResponse> Handle(CreateMemberCommand request, CancellationToken token)
        {
            //Check if employer does not exist 
            var employer = await _memberRepository.GetByIdAsync(request.employerId);
            if (employer == null)
            {
                _logger.LogWarning($"Employer with Id {request.employerId} does not exist");
                return new CreateMemberCommandResponse()
                {
                    Message = $"Employer with Id {request.employerId} does not exist"
                };
            }

            //Check if member already exists
            var memberExists = await _memberRepository.ExistsAsync(request.email);
            if(memberExists)
            {
                _logger.LogWarning($"Member with Email {request.email} already exists");
                return new CreateMemberCommandResponse()
                {
                    Message = $"Member with Email {request.email} already exists"
                };
            }

            //Check if member is btw 18-70

            //Check if member is not a minor (under 18)
            if (request.dateOfBirth.AddYears(18) > DateTime.UtcNow.Date)
            {
                _logger.LogWarning($"Member with Email {request.email} is a minor, not qualified");
                return new CreateMemberCommandResponse()
                {
                    Message = $"Member with Email {request.email} is a minor, not qualified"
                };
            }

            //Check if member is not above 70
            if (request.dateOfBirth.AddYears(70) < DateTime.UtcNow.Date)
            {
                _logger.LogWarning($"Member with Email {request.email} is above 70, not qualified");
                return new CreateMemberCommandResponse()
                {
                    Message = $"Member with Email {request.email} is above 70, not qualified"
                };
            }

            //Create Member
            var member = new Domain.Entities.Member(new Guid(), request.name, request.email, request.dateOfBirth, request.phoneNumber, request.employerId);
            if(member == null)
            {
                _logger.LogWarning($"Member with Email {request.email} not created");
                return new CreateMemberCommandResponse()
                {
                    Message = $"Member with Email {request.email} not created"
                };
            }
            await _memberRepository.CreateAsync(member);

            //Save to Db
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"{member.Name}  Created Successfully");
            return new CreateMemberCommandResponse()
            {
                IsSuccess = true,
                Message = "Member Created",
                Id = member.Id,
                Name = member.Name,
                Email = member.Email,
                DateOfBirth = member.DOB,
                PhoneNumber = member.PhoneNumber,
                EmployerId = member.EmployerId
            };
        }
    }
    
}
