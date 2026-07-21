using Microsoft.EntityFrameworkCore;
using EnterpriseCraft.Template.Modules.Customers.Domain;

namespace EnterpriseCraft.Template.Shared.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Add DbSets for domain entities used by modules
    public DbSet<Customer> Customers { get; set; } = null!;
}

