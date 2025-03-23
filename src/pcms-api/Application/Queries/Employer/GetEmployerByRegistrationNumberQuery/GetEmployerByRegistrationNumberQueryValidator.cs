using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Employer.GetEmployerByRegistrationNumberQuery
{
    public class GetEmployerByRegistrationNumberQueryValidator : AbstractValidator<GetEmployerByRegistrationNumberQuery>
    {
        public GetEmployerByRegistrationNumberQueryValidator()
        {
            RuleFor(x => x.registrationNumber).NotEmpty().WithMessage("Registration Number is required");
        }
    }
}
