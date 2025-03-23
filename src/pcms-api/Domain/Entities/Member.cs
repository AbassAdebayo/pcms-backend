using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Member : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime DOB { get; set; }
        public string PhoneNumber { get; set; }
        public Guid EmployerId { get; set; }
        public Employer Employer { get; set; }

        public Member() { }

        public Member(Guid id, string name, string email, DateTime dob, string phoneNumber, Guid employerId)
        {
            Id = id;
            Name = name;
            Email = email;
            DOB = dob;
            PhoneNumber = phoneNumber;
            EmployerId = employerId;
        }

    }
}
