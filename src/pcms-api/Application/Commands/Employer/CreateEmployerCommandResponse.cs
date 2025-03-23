using Application.Models;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Employer
{
    public class CreateEmployerCommandResponse : BaseResponse
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        public Status Status { get; set; }
        //public ICollection<Member> Members { get; set; }
    }
}
