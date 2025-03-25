using Application.Abstractions;
using Application.Queries.Contribution.ListMemberContributionsQuery;
using Domain.Contracts.Repositories;
using Domain.Wrapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Contribution.GetContributionInterestQuery
{
    public class GetContributionInterestQueryHandler : IQueryHandler<GetContributionInterestQuery, GetContributionInterestQueryResponse>
    {
        private readonly IContributionRepository _contributionRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<GetContributionInterestQueryHandler> _logger;

        public GetContributionInterestQueryHandler(IContributionRepository contributionRepository, ILogger<GetContributionInterestQueryHandler> logger,
            IMemberRepository memberRepository)
        {
            _contributionRepository = contributionRepository;
            _memberRepository = memberRepository;
            _logger = logger;
        }
        public async Task<Result<GetContributionInterestQueryResponse>> Handle(GetContributionInterestQuery request, CancellationToken cancellationToken)
        {
            //Check if member is not empty
            var member = await _memberRepository.GetByIdAsync(request.memberId);
            if (member == null)
            {
                _logger.LogError($"Member with ID {request.memberId} not found");
                return await Result<GetContributionInterestQueryResponse>.FailAsync($"Member with ID {request.memberId} not found");
            }

            //Get contribution interest
            var contributionInterest = await _contributionRepository.CalculateInterest(request.memberId);
            if(contributionInterest == 0)
            {
                _logger.LogError($"Interest for member with name {member.Name} not found");
                return await Result<GetContributionInterestQueryResponse>.FailAsync($"Interest for member with name {member.Name} not found");
            }
            return await Result<GetContributionInterestQueryResponse>.SuccessAsync(new GetContributionInterestQueryResponse(contributionInterest), $"Interest for {member.Name} fetched");
        }
    }
}
