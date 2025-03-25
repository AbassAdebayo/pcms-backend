using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Contribution.GetContributionInterestQuery
{
    public class GetContributionInterestQueryResponse : BaseResponse
    {
        public decimal Interest { get; set; }
        public GetContributionInterestQueryResponse(decimal interest)
        {
            Interest = interest;
        }
    }
}
