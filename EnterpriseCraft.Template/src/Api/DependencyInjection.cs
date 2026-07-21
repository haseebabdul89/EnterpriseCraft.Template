using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace EnterpriseCraft.Template.Api;

public static class DependencyInjection
{
    /// <summary>
    /// Register host-level, transport concerns only.
    /// Do NOT register module handlers, DbContext or MediatR here.
    /// Modules should expose their own AddXModule() extension and be wired from the composition root.
    /// </summary>
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        // CORS for browser-based API clients (adjust policy for your needs)
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        // Mapster configuration (map DTOs if you use Mapster)
        services.AddMapster();

        // Make HttpContext available for libraries that require it
        services.AddHttpContextAccessor();

        // Health checks (lightweight host probe)
        services.AddHealthChecks();

        // Keep this method focused on hosting concerns (Swagger is registered in Program.cs)
        return services;
    }

}