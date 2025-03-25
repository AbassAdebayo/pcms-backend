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
        public int RetryCount { get; set; } = 0;
        public Decimal Amount { get; set; }
        public DateTime ContributionDate { get; set; }
        public ContributionType ContributionType { get; set; }
        public ContributionStatus ContributionStatus { get; set; }



        public Contribution(Guid id, Guid memberId, decimal amount, ContributionType contributionType)
        {
            Id = id;
            MemberId = memberId;
            Amount = amount;
            ContributionType = contributionType;
            ContributionStatus = ContributionStatus.Pending;
        }

        public void ValidateContributionAmount()
        {
            if (Amount <= 0)
            {
                ContributionStatus = ContributionStatus.Failed;
                throw new Exception("Amount must be greater than zero");
            }
        }

        public void MarkAsCompleted()
        {
            ContributionStatus = ContributionStatus.Completed;
        }
        public void MarkAsFailed()
        {
            ContributionStatus = ContributionStatus.Failed;
        }

    }
}
