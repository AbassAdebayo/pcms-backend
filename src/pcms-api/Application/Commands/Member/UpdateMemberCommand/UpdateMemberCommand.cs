using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Abstractions.ICommand;

namespace Application.Commands.Member.UpdateMemberCommand
{
    public sealed record UpdateMemberCommand(Guid id, string email, string name, string phoneNumber, DateTime dateOfBirth) 
        : ICommand<UpdateMemberCommandResponse>
    {
    }
}
