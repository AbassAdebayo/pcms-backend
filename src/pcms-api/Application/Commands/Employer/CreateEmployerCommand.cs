using Application.Abstractions;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Employer
{
    public sealed record CreateEmployerCommand(string companyName) : ICommand

    { }
    
}
