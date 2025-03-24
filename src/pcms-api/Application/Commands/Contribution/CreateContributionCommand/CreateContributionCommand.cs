using Application.Abstractions;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Contribution.CreateContributionCommand
{
    public sealed record CreateContributionCommand(Guid memberId, decimal amount, ContributionType contributionType) : ICommand
    {
    }
}
