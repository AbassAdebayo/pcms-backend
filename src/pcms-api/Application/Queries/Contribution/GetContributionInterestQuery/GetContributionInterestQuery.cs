using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Contribution.GetContributionInterestQuery
{
    public sealed record GetContributionInterestQuery(Guid memberId) : IQuery<GetContributionInterestQueryResponse>
    {
    }
}
