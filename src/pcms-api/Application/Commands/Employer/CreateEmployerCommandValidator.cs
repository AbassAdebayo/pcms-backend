using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Employer
{
    public class CreateEmployerCommandValidator : AbstractValidator<CreateEmployerCommand>
    {
        public CreateEmployerCommandValidator()
        {
            RuleFor(x => x.companyName).NotEmpty().WithMessage("Company Name is required");
        }
    }
}
