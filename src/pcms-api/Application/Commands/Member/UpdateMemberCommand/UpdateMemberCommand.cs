using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Abstractions.ICommand;

namespace Application.Commands.Member.UpdateMemberCommand
{
    //public sealed record UpdateMemberCommand(Guid id, string email, string name, string phoneNumber, DateTime dateOfBirth) 
    //: ICommand { }
    public sealed record ChangeMemberNameCommand(Guid id, string name) : ICommand { }
    public sealed record ChangeMemberEmailCommand(Guid id, string email) : ICommand { }
    public sealed record ChangeMemberPhoneNumberCommand(Guid id, string phoneNumber) : ICommand { }
    public sealed record ChangeMemberDateOfBirthCommand(Guid id, DateTime dateOfBirth) : ICommand { }
}
