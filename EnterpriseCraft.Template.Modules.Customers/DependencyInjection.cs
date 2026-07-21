using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using System.Collections.Generic;

namespace EnterpriseCraft.Template.Modules.Customers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomersModule(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblies(typeof(DependencyInjection).Assembly)
                .AddClasses(classes => classes
                    .Where(type =>
                        // Skip record-like DTOs which implement IEquatable<T> (records auto-implement this)
                        !type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEquatable<>))
                        // Skip types that live in the Features namespace (common place for Commands/Responses/DTOs)
                        && !(type.Namespace?.Contains(".Features", StringComparison.Ordinal) ?? false)
                        // Only concrete, non-nested, public classes
                        && type.IsClass && !type.IsAbstract && type.IsPublic && !type.IsNested
                    ))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            // Register MediatR handlers from this module
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            // Register validators from this module
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}