using Domain.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Member.ListMembersQuery
{
    public sealed record ListMembersQueryResponse(PaginatedResult<Domain.Entities.Member> Members)
    {
    }
}
