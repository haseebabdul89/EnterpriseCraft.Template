using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FluentValidation;
using MediatR;

namespace EnterpriseCraft.Template.Modules.Customers;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomersModule(this IServiceCollection services)
    {
        services.Scan(static scan => scan
            .FromAssemblies(typeof(DependencyInjection).Assembly)
            // Exclude compiler-generated record types from being registered
            .AddClasses(classes => classes.Where(type => !IsRecordType(type)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        // Register MediatR handlers from this module
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        // Register validators from this module
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }

    // Detects C# record classes by presence of the compiler-generated EqualityContract property.
    // This is a reliable heuristic for record classes (declared on the type).
    private static bool IsRecordType(Type type)
    {
        // Only classes can be records (excluding record structs)
        if (!type.IsClass) return false;

        const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly;
        var eqProp = type.GetProperty("EqualityContract", flags);
        return eqProp != null;
    }
}