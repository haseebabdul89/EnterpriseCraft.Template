using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace EnterpriseCraft.Template.Modules.Customers;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomersModule(this IServiceCollection services)
    {
        // Register MediatR handlers from this module assembly
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        // Register validators from this module
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        // Convention-based registrations for module services/repositories
        services.Scan(scan => scan
            .FromAssemblyOf<DependencyInjection>()
            .AddClasses()
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}