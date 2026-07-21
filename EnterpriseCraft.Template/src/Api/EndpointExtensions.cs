using Microsoft.AspNetCore.Builder;

namespace EnterpriseCraft.Template.Api;

public static class EndpointExtensions
{
    /// <summary>
    /// Map host-level endpoints (health, metrics, etc.).
    /// Feature/module endpoints remain in their modules.
    /// </summary>
    public static WebApplication MapApiEndpoints(this WebApplication app)
    {
        // Health check endpoint
        app.MapHealthChecks("/health");

        // Optionally group other host endpoints here
        return app;
    }
}