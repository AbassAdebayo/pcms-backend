using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrapper
{
    public interface IPipelineBehaviour<in TRequest, TResponse> where TRequest : notnull
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next);
    }
}
