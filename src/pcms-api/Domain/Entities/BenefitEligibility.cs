using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BenefitEligibility : BaseEntity
    {
        public Guid MemberId { get; set; }
        public Member Member { get; set; }
        public DateTime EligibileFrom { get; set; }
        public EligibilityStatus EligibilityStatus { get; set; }
    }
}
