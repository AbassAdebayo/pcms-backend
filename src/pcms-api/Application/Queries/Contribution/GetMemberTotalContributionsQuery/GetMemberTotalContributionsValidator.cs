using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Contribution.GetMemberTotalContributionsQuery
{
    public class GetMemberTotalContributionsValidator : AbstractValidator<GetMemberTotalContributionsQuery>
    {
        public GetMemberTotalContributionsValidator() 
        {
            RuleFor(x => x.memberId).NotEmpty().WithMessage("Member ID is required");
        }
    }
}
