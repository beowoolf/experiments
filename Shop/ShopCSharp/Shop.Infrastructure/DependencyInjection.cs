using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shop.Application.Common.Interfaces;
using Shop.Infrastructure.Persistence;
using Shop.Infrastructure.Services;
using System;

namespace Shop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, 
            IConfiguration configuration,
            ILoggerFactory loggerFactory)
        {
            services.AddTransient<IDateTime, DateTimeService>();

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"))
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                .UseLoggerFactory(loggerFactory)
                .EnableSensitiveDataLogging());

            return services;
        }
    }
}
