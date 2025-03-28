﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class BadGatewayException : Exception
    {
        public BadGatewayException(string message)
            : base(message)
        { }

        public BadGatewayException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
