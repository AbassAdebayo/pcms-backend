using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Employer
{
    class GetEmployerByIdQueryValidator : AbstractValidator<GetEmployerByIdQuery>
    {
        public GetEmployerByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Employer Id is required");
        }
    }
}
