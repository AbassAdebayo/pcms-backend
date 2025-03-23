﻿using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Member
{
    public class CreateMemberCommandResponse : BaseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public string PhoneNumber { get; set; }
        public Guid EmployerId { get; set; }
    }
}
