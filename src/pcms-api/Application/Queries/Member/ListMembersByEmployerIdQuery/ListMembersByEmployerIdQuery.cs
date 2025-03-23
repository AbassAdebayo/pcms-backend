using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Member.ListMembersByEmployerIdQuery
{
    public sealed record ListMembersByEmployerIdQuery(Guid employerId, int page, int pageSize) : IQuery<ListMembersByEmployerIdQueryResponse>
    {
    }
}
