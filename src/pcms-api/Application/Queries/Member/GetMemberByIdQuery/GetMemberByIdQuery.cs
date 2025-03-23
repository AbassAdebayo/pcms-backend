using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Member.GetMemberByIdQuery
{
    public sealed record GetMemberByIdQuery(Guid Id) : IQuery<GetMemberByIdQueryResponse>
    {
    }
}
