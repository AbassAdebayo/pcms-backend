using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Contribution.ListMemberContributionsQuery
{
    public sealed record ListMemberContributionsQuery(Guid memberId, int page, int pageSize) : IQuery<ListMemberContributionsResponse>
    {
    }
}
