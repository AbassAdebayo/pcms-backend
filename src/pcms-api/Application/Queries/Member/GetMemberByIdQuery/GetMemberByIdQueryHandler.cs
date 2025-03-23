using Application.Abstractions;
using Domain.Contracts.Repositories;
using Domain.Wrapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Member.GetMemberByIdQuery
{
    public class GetMemberByIdQueryHandler : IQueryHandler<GetMemberByIdQuery, GetMemberByIdQueryResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<GetMemberByIdQueryHandler> _logger;

        public GetMemberByIdQueryHandler(IMemberRepository memberRepository, ILogger<GetMemberByIdQueryHandler> logger)
        {
            _memberRepository = memberRepository;
            _logger = logger;
        }
        public async Task<Result<GetMemberByIdQueryResponse>> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            //Check if member does not exist
            var member = await _memberRepository.GetByIdAsync(request.Id);
            if (member == null)
            {
                _logger.LogWarning($"Member with Id {request.Id} does not exist");
                return await Result<GetMemberByIdQueryResponse>.FailAsync($"Member with Id {request.Id} does not exist");
            }

            var data = new GetMemberByIdQueryResponse(member.Id, member.Name, member.PhoneNumber, member.Email, member.DOB, member.EmployerId);

            return await Result<GetMemberByIdQueryResponse>.SuccessAsync(data);
        }
    }
 
}
