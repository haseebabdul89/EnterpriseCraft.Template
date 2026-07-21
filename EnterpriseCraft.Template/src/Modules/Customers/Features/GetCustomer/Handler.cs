using MediatR;
using Microsoft.EntityFrameworkCore;
using EnterpriseCraft.Template.Modules.Customers.Domain;
using EnterpriseCraft.Template.Shared.Persistence;

namespace EnterpriseCraft.Template.Modules.Customers.Features.GetCustomer;

/// <summary>
/// Handles <see cref="Query"/> by reading from the AppDbContext.
/// </summary>
public sealed class Handler : IRequestHandler<Query, Response?>
{
    private readonly AppDbContext _db;

    public Handler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Response?> Handle(Query request, CancellationToken cancellationToken)
    {
        // Ensure the DbContext exposes DbSet<Customer> Customers.
        // Filters out soft-deleted entities if your model uses IsDeleted.
        var dto = await _db.Set<Customer>()
            .AsNoTracking()
            .Where(c => c.Id == request.Id && !c.IsDeleted)
            .Select(c => new Response(c.Id, c.Name, c.Email, c.Phone))
            .FirstOrDefaultAsync(cancellationToken);

        return dto;
    }
}