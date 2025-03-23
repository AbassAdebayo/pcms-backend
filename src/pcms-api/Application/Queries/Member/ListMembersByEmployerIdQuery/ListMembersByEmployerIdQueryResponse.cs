using Domain.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Member.ListMembersByEmployerIdQuery
{
    public sealed record ListMembersByEmployerIdQueryResponse(PaginatedResult<Domain.Entities.Member> Members)
    {
    }
}
