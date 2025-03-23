using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Member
{
    public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
    {
        public CreateMemberCommandValidator()
        {
            RuleFor(x => x.name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.phoneNumber).NotEmpty().WithMessage("Phone number is required");
            RuleFor(x => x.employerId).NotEmpty().WithMessage("Employer ID is required");
            RuleFor(x => x.email).NotEmpty().WithMessage("Email is required");
        }
    }
}
