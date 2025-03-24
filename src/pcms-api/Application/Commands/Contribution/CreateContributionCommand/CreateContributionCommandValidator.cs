using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Contribution.CreateContributionCommand
{
    public class CreateContributionCommandValidator : AbstractValidator<CreateContributionCommand>
    {
        public CreateContributionCommandValidator() 
        {
            RuleFor(x => x.memberId).NotEmpty().WithMessage("Member Id is required");
            RuleFor(x => x.amount).NotEmpty().GreaterThan(0).WithMessage("Amount must be greater than zero");
            RuleFor(x => x.contributionType).IsInEnum().WithMessage("Invalid Contribution Type");
            RuleFor(x => x.contributionType).NotEmpty().WithMessage("Contribution Type is required");

        }
    }
}
