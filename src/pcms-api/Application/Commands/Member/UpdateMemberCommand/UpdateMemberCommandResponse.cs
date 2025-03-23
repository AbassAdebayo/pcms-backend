using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Member.UpdateMemberCommand
{
    public sealed record UpdateMemberCommandResponse(Guid id, string name, string email, DateTime dateOfBirth, string phoneNumber, Guid employerId)

    { }
}
