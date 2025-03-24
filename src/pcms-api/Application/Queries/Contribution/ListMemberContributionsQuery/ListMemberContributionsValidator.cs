using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Contribution.ListMemberContributionsQuery
{
    public class ListMemberContributionsValidator : AbstractValidator<ListMemberContributionsQuery>
    {
        public ListMemberContributionsValidator() 
        {
            RuleFor(x => x.memberId).NotEmpty().WithMessage("Member ID is required");
            RuleFor(x => x.page).GreaterThan(0).WithMessage("Page must be greater than 0");
            RuleFor(x => x.pageSize).GreaterThan(0).WithMessage("Page size must be greater than 0");
        }
    }
}
