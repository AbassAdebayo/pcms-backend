using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entities
{
    public class Employer : BaseEntity
    {
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        public Status Status { get; set; } = Status.Active;
        public ICollection<Member> Members { get; set; } = new HashSet<Member>();

        public Employer(Guid id, string companyName, string registrtaionNumber, Status status)
        {
            Id = id;
            CompanyName = companyName;
            RegistrationNumber = registrtaionNumber;
            Status = status;

        }

    }

    
}
