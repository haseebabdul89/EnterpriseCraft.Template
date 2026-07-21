namespace EnterpriseCraft.Template
{
    using System;
    using System.Linq;
    using FluentValidation;
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using EnterpriseCraft.Template.Api;
    using EnterpriseCraft.Template.Shared.Persistence;
    using EnterpriseCraft.Template.Shared.Exceptions;
    using EnterpriseCraft.Template.Shared.Behaviors;
    using EnterpriseCraft.Template.Modules.Customers;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Cross-cutting services: controllers, minimal API metadata and Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Database connection logic (Development prefers configured DefaultConnection / user-secrets)
            string? conn = builder.Environment.IsDevelopment()
                ? builder.Configuration.GetConnectionString("DefaultConnection")
                : Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                  ?? builder.Configuration.GetConnectionString("DefaultConnection");

            if (!string.IsNullOrWhiteSpace(conn))
            {
                builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(conn));
            }
            else
            {
                // fallback for local dev / CI
                builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("AppDb"));
            }

            // Module registrations (each module owns its DI)
            builder.Services.AddCustomersModule();

            // MediatR - aggregate handlers from root and module assemblies
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                typeof(Program).Assembly,
                typeof(EnterpriseCraft.Template.Modules.Customers.DependencyInjection).Assembly));

            // FluentValidation - register validators from root and module assemblies
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddValidatorsFromAssembly(typeof(EnterpriseCraft.Template.Modules.Customers.DependencyInjection).Assembly);

            // Pipeline behaviors (validation + logging) implemented under src/Shared/Behaviors
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            // Keep Api composition root for additional cross-cutting DI (Mapster, Serilog, etc.)
            builder.Services.AddApi(builder.Configuration);

            // Program.cs (temporary diagnostic snippet)
            var loaded = AppDomain.CurrentDomain.GetAssemblies()
                .Select(a => new { a.GetName().Name, Version = a.GetName().Version?.ToString(), Location = GetLocationSafe(a) })
                .OrderBy(a => a.Name)
                .ToList();

            foreach (var a in loaded)
            {
                Console.WriteLine($"{a.Name} | {a.Version} | {a.Location}");
            }

            static string? GetLocationSafe(System.Reflection.Assembly a)
            {
                try { return a.Location; } catch { return null; }
            }
            var app = builder.Build();

            // Global exception middleware early in pipeline
            app.UseMiddleware<GlobalExceptionMiddleware>();

            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnterpriseCraft Template API v1");
                    c.RoutePrefix = "swagger"; // access at /swagger
                });
            }

            //app.MapGet("/", () => Results.Ok("API running"));

            app.UseHttpsRedirection();
            app.UseAuthorization();
            //app.MapControllers();
            app.MapCustomerEndpoints(); // Map endpoints from the Customers module

            app.Run();
        }
    }
}
