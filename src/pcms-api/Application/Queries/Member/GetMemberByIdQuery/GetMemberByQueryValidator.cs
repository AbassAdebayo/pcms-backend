using FluentValidation;

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
