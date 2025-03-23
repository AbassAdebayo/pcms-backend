using Application.Abstractions;
using Application.Commands.Member.CreateMemberCommand;
using Domain.Contracts.Repositories;
using Domain.Wrapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Abstractions.ICommand;

namespace Application.Commands.Member.UpdateMemberCommand
{
    public class UpdateMemberCommandHandler : ICommandHandler<UpdateMemberCommand, UpdateMemberCommandResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<UpdateMemberCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMemberCommandHandler(IMemberRepository memberRepository, ILogger<UpdateMemberCommandHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<UpdateMemberCommandResponse>> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            //Check if member does not exist
            var member = await _memberRepository.GetByIdAsync(request.id);
            if (member == null)
            {
                _logger.LogWarning($"Member with Id {request.id} does not exist");
                return await Result<UpdateMemberCommandResponse>.FailAsync($"Member with Id {request.id} does not exist");
            }

            //Check if member already exists
            var memberExists = await _memberRepository.ExistsAsync(request.email);
            if (memberExists)
            {
                _logger.LogWarning($"Member with Email {request.email} already exists");
                return await Result<UpdateMemberCommandResponse>.FailAsync($"Member with Email {request.email} already exists");
            }

            //Check if member is btw 18-70
            //Check if member is not a minor (under 18)
            if (request.dateOfBirth.AddYears(18) > DateTime.UtcNow.Date)
            {
                _logger.LogWarning($"Member with Email {request.email} is a minor, not qualified");
                return await Result<UpdateMemberCommandResponse>.FailAsync($"Member with Email {request.email} is a minor, not qualified");
            }

            //Check if member is not above 70
            if (request.dateOfBirth.AddYears(70) < DateTime.UtcNow.Date)
            {
                return await Result<UpdateMemberCommandResponse>.FailAsync($"Member with Email {request.email} is above 70, not qualified");
            }

            //Swap data to new data
            member.UpdateMember(request.name, request.email, request.dateOfBirth, request.phoneNumber);

            //Save to Db
            var result = await _memberRepository.UpdateAsync(request.id, member);
            await _unitOfWork.SaveChangesAsync();

            if (result == null)
            {
                _logger.LogWarning($"Member with Id {request.id} not updated");
                return await Result<UpdateMemberCommandResponse>.FailAsync($"Member with Id {request.id} not updated");
            }

            var data = new UpdateMemberCommandResponse(result.Id, result.Name, result.Email, result.DOB, result.PhoneNumber, result.EmployerId);

            //Return response
            return await Result<UpdateMemberCommandResponse>.SuccessAsync(data);


        }
    }
}
