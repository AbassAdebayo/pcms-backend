using Application.Extensions.NotificationService;
using Domain.Contracts.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services

                // Add Repository
                .AddScoped<IMemberRepository, MemberRepository>()
                .AddScoped<IEmployerRepository, EmployerRepository>()
                .AddScoped<IContributionRepository, ContributionRepository>()

                //Add Notification Service
                .AddScoped<INotificationService, EmailNotificationService>()

                .AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            return services;
        }
    }
}
