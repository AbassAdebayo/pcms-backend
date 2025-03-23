using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Member.ListMembersQuery
{
    public class ListMembersQueryValidator : AbstractValidator<ListMembersQuery>
    {
        public ListMembersQueryValidator()
        {
            RuleFor(x => x.page).GreaterThan(0).WithMessage("Page must be greater than 0");
            RuleFor(x => x.pageSize).GreaterThan(0).WithMessage("Page size must be greater than 0");
        }
    }
}
