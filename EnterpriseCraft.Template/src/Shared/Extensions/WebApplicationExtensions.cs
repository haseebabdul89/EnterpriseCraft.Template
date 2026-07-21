using Microsoft.AspNetCore.Builder;

namespace EnterpriseCraft.Template.Shared.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseShared(this WebApplication app) => app;
}
