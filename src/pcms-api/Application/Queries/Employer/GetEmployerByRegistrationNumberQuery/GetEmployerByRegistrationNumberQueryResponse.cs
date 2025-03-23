using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Employer.GetEmployerByRegistrationNumberQuery
{
    public sealed record GetEmployerByRegistrationNumberQueryResponse(Guid Id, string CompanyName, string registrationNumber, Status status)
    {
    }
}
