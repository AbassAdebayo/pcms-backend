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
    public class ChangeMemberNameCommandHandler : ICommandHandler<ChangeMemberNameCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<ChangeMemberNameCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeMemberNameCommandHandler(IMemberRepository memberRepository, ILogger<ChangeMemberNameCommandHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ChangeMemberNameCommand request, CancellationToken cancellationToken)
        {
            //Check if member does not exist
            var member = await _memberRepository.GetByIdAsync(request.id);
            if (member == null)
            {
                _logger.LogWarning($"Member with Id {request.id} does not exist");
                return await Result<UpdateMemberCommandResponse>.FailAsync($"Member with Id {request.id} does not exist");
            }

            //Update member's name
            member.ChangeName(request.name);

            //Save to Db
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"Member's name updated successfully");

            return await Result<string>.SuccessAsync($"Member's name updated successfully");
        }

    }

    public class ChangeMemberEmailCommandHandler : ICommandHandler<ChangeMemberEmailCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<ChangeMemberEmailCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeMemberEmailCommandHandler(IMemberRepository memberRepository, ILogger<ChangeMemberEmailCommandHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ChangeMemberEmailCommand request, CancellationToken cancellationToken)
        {
            //Check if member does not exist
            var member = await _memberRepository.GetByIdAsync(request.id);
            if (member == null)
            {
                _logger.LogWarning($"Member with Id {request.id} does not exist");
                return await Result<UpdateMemberCommandResponse>.FailAsync($"Member with Id {request.id} does not exist");
            }

            //Update member's email
            member.ChangeEmail(request.email);

            //Save to Db
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"Member's email updated successfully");

            return await Result<string>.SuccessAsync($"Member's email updated successfully");
        }

    }

    public class ChangeMemberPhoneNumberCommandHandler : ICommandHandler<ChangeMemberPhoneNumberCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<ChangeMemberPhoneNumberCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public ChangeMemberPhoneNumberCommandHandler(IMemberRepository memberRepository, ILogger<ChangeMemberPhoneNumberCommandHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(ChangeMemberPhoneNumberCommand request, CancellationToken cancellationToken)
        {
            //Check if member does not exist
            var member = await _memberRepository.GetByIdAsync(request.id);
            if (member == null)
            {
                _logger.LogWarning($"Member with Id {request.id} does not exist");
                return await Result<UpdateMemberCommandResponse>.FailAsync($"Member with Id {request.id} does not exist");
            }

            //Update member's phone number
            member.ChangePhoneNumber(request.phoneNumber);

            //Save to Db
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"Member's phone number updated successfully");

            return await Result<string>.SuccessAsync($"Member's phone number updated successfully");
        }
    }

    public class ChangeMemberDateOfBirthCommandHandler : ICommandHandler<ChangeMemberDateOfBirthCommand>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<ChangeMemberDateOfBirthCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public ChangeMemberDateOfBirthCommandHandler(IMemberRepository memberRepository, ILogger<ChangeMemberDateOfBirthCommandHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(ChangeMemberDateOfBirthCommand request, CancellationToken cancellationToken)
        {
            //Check if member does not exist
            var member = await _memberRepository.GetByIdAsync(request.id);
            if (member == null)
            {
                _logger.LogWarning($"Member with Id {request.id} does not exist");
                return await Result<string>.FailAsync($"Member with Id {request.id} does not exist");
            }



            //Check if member is btw 18-70
            //Check if member is not a minor (under 18)
            if (request.dateOfBirth.AddYears(18) > DateTime.UtcNow.Date)
            {
                _logger.LogWarning($"Member with ID {request.id} is a minor, not qualified");
                return await Result<string>.FailAsync($"Member with ID  {request.id} is a minor, not qualified");
            }

            //Check if member is not above 70
            if (request.dateOfBirth.AddYears(70) < DateTime.UtcNow.Date)
            {
                return await Result<string>.FailAsync($"Member with ID  {request.id} is above 70, not qualified");
            }

            //Update member's date of birth
            member.ChangeDateOfBirth(request.dateOfBirth);

            //Save to Db
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"Member's date of birth updated successfully");

            return await Result<string>.SuccessAsync($"Member's date of birth updated successfully");

        }
    }

}