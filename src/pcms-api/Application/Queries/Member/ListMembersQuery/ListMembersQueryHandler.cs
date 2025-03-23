using Application.Abstractions;
using Domain.Contracts.Repositories;
using Domain.Wrapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Member.ListMembersQuery
{
    public class ListMembersQueryHandler : IQueryHandler<ListMembersQuery, ListMembersQueryResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<ListMembersQueryHandler> _logger;

        public ListMembersQueryHandler(IMemberRepository memberRepository, ILogger<ListMembersQueryHandler> logger)
        {
            _memberRepository = memberRepository;
            _logger = logger;
        }
        public async Task<Result<ListMembersQueryResponse>> Handle(ListMembersQuery request, CancellationToken cancellationToken)
        {
            //Fetch list of members
            var members = await _memberRepository.ListAsync(request.page, request.pageSize);
            
            //Map data
            var data = new ListMembersQueryResponse(members);

            return await Result<ListMembersQueryResponse>.SuccessAsync(data);

        }
    }
}
