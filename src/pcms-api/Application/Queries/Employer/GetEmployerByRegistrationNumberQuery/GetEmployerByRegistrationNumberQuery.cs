using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Employer.GetEmployerByRegistrationNumberQuery
{
    public sealed record GetEmployerByRegistrationNumberQuery(string registrationNumber) : IQuery<GetEmployerByRegistrationNumberQueryResponse>
    {
    }
}
