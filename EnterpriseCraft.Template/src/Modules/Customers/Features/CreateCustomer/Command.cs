using MediatR;

namespace EnterpriseCraft.Template.Modules.Customers.Features.CreateCustomer;

public sealed record Command(string Name, string Email, string? Phone) : IRequest<Response>;
