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

namespace Application.Queries.Contribution.GetMemberTotalContributionsQuery
{
    public class GetMemberTotalContributionsHandler : IQueryHandler<GetMemberTotalContributionsQuery, GetMemberTotalContributionsResponse>
    {
        private readonly IContributionRepository _contributionRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<ListMemberContributionsHandler> _logger;

        public GetMemberTotalContributionsHandler(IContributionRepository contributionRepository, ILogger<ListMemberContributionsHandler> logger,
            IMemberRepository memberRepository)
        {
            _contributionRepository = contributionRepository;
            _memberRepository = memberRepository;
            _logger = logger;
        }
        public async Task<Result<GetMemberTotalContributionsResponse>> Handle(GetMemberTotalContributionsQuery request, CancellationToken cancellationToken)
        {
            //Check if member is not empty
            var member = await _memberRepository.GetByIdAsync(request.memberId);
            if (member == null)
            {
                _logger.LogError($"Member with ID {request.memberId} not found");
                return await Result<GetMemberTotalContributionsResponse>.FailAsync($"Member with ID {request.memberId} not found");
            }

            //Get member total contributions
            var totalContributions = await _contributionRepository.TotalContributions(request.memberId);

            if (totalContributions == 0)
            {
                _logger.LogError($"Total contributions for member with name {member.Name} not found");
                return await Result<GetMemberTotalContributionsResponse>.FailAsync($"Total contributions for member with name {member.Name} not found");
            }

            return await Result<GetMemberTotalContributionsResponse>.SuccessAsync(new GetMemberTotalContributionsResponse(totalContributions), $"Total contributions for {member.Name} fetched");
        }
    }
}
