using Application.Middlewares;
using Application.Wrapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return services
                .AddMediatR(a => a.RegisterServicesFromAssembly(assembly))
                .AddTransient(typeof(IPipelineBehaviour<,>), typeof(ValidationBehavior<,>))
                .AddValidatorsFromAssembly(assembly)
                .AddTransient<ExceptionHandlingMiddleware>();
        }
    }
}
