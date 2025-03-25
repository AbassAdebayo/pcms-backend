using Application.Abstractions;
using Application.Queries.Member.ListMembersQuery;
using Domain.Contracts.Repositories;
using Domain.Wrapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Contribution.ListMemberContributionsQuery
{
    public class ListMemberContributionsHandler : IQueryHandler<ListMemberContributionsQuery, ListMemberContributionsResponse>
    {
        private readonly IContributionRepository _contributionRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<ListMemberContributionsHandler> _logger;

        public ListMemberContributionsHandler(IContributionRepository contributionRepository, ILogger<ListMemberContributionsHandler> logger,
            IMemberRepository memberRepository)
        {
            _contributionRepository = contributionRepository;
            _memberRepository = memberRepository;
            _logger = logger;
        }
        public async Task<Result<ListMemberContributionsResponse>> Handle(ListMemberContributionsQuery request, CancellationToken cancellationToken)
        {
           //Check if member is not empty
           var member = await _memberRepository.GetByIdAsync(request.memberId);
            if(member == null)
            {
                _logger.LogError($"Member with ID {request.memberId} not found");
                return await Result<ListMemberContributionsResponse>.FailAsync($"Member with ID {request.memberId} not found");
            }

            //Get member contributions
            var membersContributions = await _contributionRepository.ListAsync(request.memberId, request.page, request.pageSize);

            if (membersContributions == null)
            {
                _logger.LogError($"Contributions for member with name {member.Name} not found");
                return await Result<ListMemberContributionsResponse>.FailAsync($"Contributions for member with name {member.Name} not found");
            }

            var data = new ListMemberContributionsResponse(membersContributions);

            return await Result<ListMemberContributionsResponse>.SuccessAsync(data, $"Contributions for {member.Name} found");
        }
    }
}
