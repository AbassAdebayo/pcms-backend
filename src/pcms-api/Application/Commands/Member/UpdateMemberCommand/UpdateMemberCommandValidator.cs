using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Member.UpdateMemberCommand
{
    public class ChangeMemberNameCommandValidator : AbstractValidator<ChangeMemberNameCommand>
    {
        public ChangeMemberNameCommandValidator()
        {
            RuleFor(x => x.name).NotEmpty().WithMessage("Name is required");
        }
    }

    public class ChangeMemberEmailCommandValidator : AbstractValidator<ChangeMemberEmailCommand>
    {
        public ChangeMemberEmailCommandValidator()
        {
            RuleFor(x => x.email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.email).EmailAddress().WithMessage("Email is invalid");
        }
    }

    public class ChangeMemberPhoneNumberCommandValidator : AbstractValidator<ChangeMemberPhoneNumberCommand>
    {
        public ChangeMemberPhoneNumberCommandValidator()
        {
            RuleFor(x => x.phoneNumber).NotEmpty().WithMessage("Phone number is required");
        }
    }

    public class ChangeMemberDateOfBirthCommandValidator : AbstractValidator<ChangeMemberDateOfBirthCommand>
    {
        public ChangeMemberDateOfBirthCommandValidator()
        {
            RuleFor(x => x.dateOfBirth).NotEmpty().WithMessage("Date of birth is required");
            RuleFor(x => x.dateOfBirth).LessThan(DateTime.Now).WithMessage("Date of birth cannot be in the future");
        }
    }
}
