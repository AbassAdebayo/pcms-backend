using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Contribution : BaseEntity
    {
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
        public Decimal Amount { get; set; }
        public DateTime ContributionDate { get; set; }
        public ContributionType ContributionType { get; set; }



        public void ValidateContributionAmount()
        {
            if (Amount <= 0)
            {
                throw new Exception("Amount must be greater than zero");
            }
        }

    }
}
