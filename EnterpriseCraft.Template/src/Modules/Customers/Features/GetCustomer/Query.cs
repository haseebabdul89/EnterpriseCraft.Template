using System;
using MediatR;

namespace EnterpriseCraft.Template.Modules.Customers.Features.GetCustomer;

/// <summary>
/// Query to get a customer by id. Sent via IMediator.
/// </summary>
public sealed record Query(Guid Id) : IRequest<Response?>;

/// <summary>
/// DTO returned by the GetCustomer query.
/// </summary>
public sealed record Response(Guid Id, string Name, string Email, string? Phone);