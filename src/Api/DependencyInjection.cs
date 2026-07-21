using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EnterpriseCraft.Template.Shared.Persistence;
using EnterpriseCraft.Template.Modules.Customers;

namespace EnterpriseCraft.Template.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext (SQL Server if connection string present, otherwise in-memory for dev/tests)
        var conn = configuration.GetConnectionString("DefaultConnection");
        if (!string.IsNullOrWhiteSpace(conn))
        {
            services.AddDbContext<AppDbContext>(o => o.UseSqlServer(conn));
        }
        else
        {
            services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("AppDb"));
        }

        // Cross-cutting registrations (Mapster, Serilog, pipeline behaviors, etc.) can go here
        // e.g. services.AddMapster(); services.AddSingleton<IMapper, ServiceMapper>();

        // Register modules (each module wires its own MediatR handlers, validators and services)
        services.AddCustomersModule();

        return services;
    }
}