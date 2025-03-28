﻿using Application.Abstractions;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Wrapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Contribution.CreateContributionCommand
{
    public class CreateContributionCommandHandler : ICommandHandler<CreateContributionCommand>
    {
        private readonly IContributionRepository _contributionRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<CreateContributionCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CreateContributionCommandHandler(IContributionRepository contributionRepository, ILogger<CreateContributionCommandHandler> logger,
            IMemberRepository memberRepository, IUnitOfWork unitOfWork)
        {
            _contributionRepository = contributionRepository;
            _memberRepository = memberRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(CreateContributionCommand request, CancellationToken token)
        {
            //Check if member exists
            var member = await _memberRepository.GetByIdAsync(request.memberId);
            if (member == null)
            {
                _logger.LogWarning($"Member with Id {request.memberId} does not exist");
                return await Result<string>.FailAsync($"Member with Id {request.memberId} does not exist");
            }

            //Check if it's a monthly contribution
            if (request.contributionType == Domain.Enums.ContributionType.MonthlyContributions)
            {
                var hasMonthly = await _contributionRepository.HasMonthlyContribution(request.memberId, DateTime.UtcNow.Date);
                if (hasMonthly)
                {
                    _logger.LogWarning($"Member with Id {request.memberId} has already made a monthly contribution for this month");
                    return await Result<string>.FailAsync($"Member with Id {request.memberId} has already made a monthly for this month");
                }
            }

            //Add contribution
            var contribution = new Domain.Entities.Contribution(new Guid(), request.memberId, request.amount, request.contributionType);

            try
            {
                //Validate amount, update status if invalid
                contribution.ValidateContributionAmount();  

                await _contributionRepository.CreateAsync(contribution);

                //Mark as completed after successful save
                contribution.MarkAsCompleted();

                //Save to Db
                await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation($"{request.amount} contribution for {member.Name} Created Successfully");

                var data = new CreateContributionCommandResponse
                {
                    Id = contribution.Id,
                    MemberId = contribution.MemberId,
                    Amount = contribution.Amount,
                    ContributionType = contribution.ContributionType,
                    ContributionDate = contribution.ContributionDate,
                    IsSuccess = true
                };

                return await Result<CreateContributionCommandResponse>.SuccessAsync(data, $"{request.amount} contribution for {member.Name} created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing contribution for Member {member.Name}: {ex.Message}");

                //Mark contribution as failed
                contribution.MarkAsFailed();
                await _unitOfWork.SaveChangesAsync();

                return await Result<string>.FailAsync($"Failed to process contribution for {member.Name}");
            }



        }

    }
}
