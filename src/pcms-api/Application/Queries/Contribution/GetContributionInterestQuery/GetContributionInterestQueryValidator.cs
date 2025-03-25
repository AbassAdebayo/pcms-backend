using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Contribution.GetContributionInterestQuery
{
    public class GetContributionInterestQueryValidator : AbstractValidator<GetContributionInterestQuery>
    {
        public GetContributionInterestQueryValidator()
        {
            RuleFor(x => x.memberId).NotEmpty().WithMessage("Member ID cannot be empty");
        }
    }
}
