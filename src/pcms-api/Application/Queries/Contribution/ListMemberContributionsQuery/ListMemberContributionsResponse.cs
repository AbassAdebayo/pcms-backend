using Application.Models;
using Domain.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Contribution.ListMemberContributionsQuery
{
    public class ListMemberContributionsResponse : BaseResponse
    {
        public PaginatedResult<Domain.Entities.Contribution> Contributions { get; set; }
        public ListMemberContributionsResponse(PaginatedResult<Domain.Entities.Contribution> contributions)
        {
            Contributions = contributions;
        }
    }
}
