﻿using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Member.DeleteMemberCommand
{
    public sealed record DeleteMemberCommand(Guid id) : ICommand
    {
    }
}
