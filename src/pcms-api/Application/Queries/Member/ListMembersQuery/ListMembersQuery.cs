using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Member.ListMembersQuery
{
    public sealed record ListMembersQuery(int page, int pageSize) : IQuery<ListMembersQueryResponse>
    {
    }
}
