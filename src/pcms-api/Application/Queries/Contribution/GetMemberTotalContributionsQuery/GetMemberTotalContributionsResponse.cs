using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Contribution.GetMemberTotalContributionsQuery
{
    public class GetMemberTotalContributionsResponse : BaseResponse
    {
        public decimal TotalContributions { get; set; }
        public GetMemberTotalContributionsResponse(decimal totalContributions)
        {
            TotalContributions = totalContributions;
        }
    }
}
