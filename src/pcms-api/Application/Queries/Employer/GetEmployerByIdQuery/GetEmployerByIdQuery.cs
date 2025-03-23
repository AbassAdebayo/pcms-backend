using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Employer
{
    public sealed record GetEmployerByIdQuery(Guid Id) : IQuery<GetEmployerByIdQueryResponse>
    {
    }
}
