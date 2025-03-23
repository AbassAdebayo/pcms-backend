using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Member.GetMemberByIdQuery
{
    public class GetMemberByQueryValidator  : AbstractValidator<GetMemberByIdQuery>
    {
        public GetMemberByQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Member ID is required");
        }
    }
}
