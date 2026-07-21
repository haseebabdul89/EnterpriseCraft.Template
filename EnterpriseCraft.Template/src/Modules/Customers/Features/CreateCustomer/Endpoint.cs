using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using MediatR;
using EnterpriseCraft.Template.Modules.Customers.Features.CreateCustomer;

namespace EnterpriseCraft.Template.Modules.Customers;

public static class CustomerEndpoints
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/v1/customers")
            .WithOpenApi();

        // Create customer - minimal endpoint forwarding to MediatR
        group.MapPost("/", async (IMediator mediator, Features.CreateCustomer.Command command) =>
        {
            var response = await mediator.Send(command);
            return Results.Created($"/api/v1/customers/{response.Id}", response);
        })
        .WithName("CreateCustomer")
        .Accepts<Features.CreateCustomer.Command>("application/json")
        .Produces<Features.CreateCustomer.Response>(statusCode: 201);

        // Example: get by id (used for Created Location)
        //group.MapGet("/{id:guid}", static async (IMediator mediator, Guid id) =>
        //{
        //    var resp = await mediator.Send(new GetCustomer(id));
        //    return resp is null ? Results.NotFound() : Results.Ok(resp);
        //})
        //.WithName("GetCustomer");

        return endpoints;
    }
}