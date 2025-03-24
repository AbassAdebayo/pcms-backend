using Application.Models;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Contribution.CreateContributionCommand
{
    public class CreateContributionCommandResponse : BaseResponse
    {
        public Guid Id { get; set; }
        public Guid MemberId { get; set; }
        public decimal Amount { get; set; }
        public ContributionType ContributionType { get; set; }
        public DateTime ContributionDate { get; set; }
    }
}
