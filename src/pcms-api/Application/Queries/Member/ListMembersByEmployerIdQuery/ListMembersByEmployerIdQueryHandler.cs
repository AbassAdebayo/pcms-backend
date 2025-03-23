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

namespace Application.Queries.Member.ListMembersByEmployerIdQuery
{
    public class ListMembersByEmployerIdQueryHandler : IQueryHandler<ListMembersByEmployerIdQuery, ListMembersByEmployerIdQueryResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<ListMembersByEmployerIdQueryHandler> _logger;
        public ListMembersByEmployerIdQueryHandler(IMemberRepository memberRepository, ILogger<ListMembersByEmployerIdQueryHandler> logger)
        {
            _memberRepository = memberRepository;
            _logger = logger;
        }

        public async Task<Result<ListMembersByEmployerIdQueryResponse>> Handle(ListMembersByEmployerIdQuery request, CancellationToken cancellationToken)
        {
            //Fetch list of members by employer
            var members = await _memberRepository.ListAsync(request.page, request.pageSize, request.employerId);

            //Map data
            var data = new ListMembersByEmployerIdQueryResponse(members);

            return await Result<ListMembersByEmployerIdQueryResponse>.SuccessAsync(data);
        }
    }
}
