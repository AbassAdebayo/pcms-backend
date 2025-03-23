using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Member.GetMemberByIdQuery
{
    public sealed record GetMemberByIdQueryResponse(Guid id, string name, string phoneNumber, string email, DateTime dateOfBirth, Guid employerId)
    {
    }
}
