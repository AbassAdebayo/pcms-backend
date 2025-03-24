using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Contribution.GetMemberTotalContributionsQuery
{
    public sealed record GetMemberTotalContributionsQuery(Guid memberId) : IQuery<GetMemberTotalContributionsResponse>
    {
    }
}
