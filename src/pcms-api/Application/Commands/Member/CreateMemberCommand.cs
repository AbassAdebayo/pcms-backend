using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Member
{
    public sealed record CreateMemberCommand(Guid employerId, string name, string email, DateTime dateOfBirth, string phoneNumber) : ICommand
    {
    }
}
