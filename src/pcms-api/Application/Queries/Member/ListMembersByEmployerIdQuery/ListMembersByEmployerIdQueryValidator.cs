using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Member.ListMembersByEmployerIdQuery
{
    public class ListMembersByEmployerIdQueryValidator : AbstractValidator<ListMembersByEmployerIdQuery>
    {
        public ListMembersByEmployerIdQueryValidator()
        {
            RuleFor(x => x.page).GreaterThan(0).WithMessage("Page must be greater than 0");
            RuleFor(x => x.pageSize).GreaterThan(0).WithMessage("Page size must be greater than 0");
            RuleFor(x => x.employerId).NotEmpty().WithMessage("Employer ID is required");
        }
    }
}
